using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour {

    public float moveSpeed = 6.0f;
    public float jumpForce = 2000.0f;

    public bool leftStart;
    bool isLeftMove;

    bool isGround = false;
    bool isJumping = false;
    bool isStop = false;

    public Transform[] groundCheck;
    public Transform[] sideCheck;

    float elapsedTime = 0;

    KeyCode turnKeyCode = KeyCode.L;
    KeyCode jumpKeyCode = KeyCode.A;

    void Awake()
    {
        isLeftMove = leftStart;
    }

    void Update()
    {
        CheckGround();

        if (isGround)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            isJumping = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime < 1)
        {
            return;
        }

        if (isStop == false)
        {
            ProcessAutoMove(Time.deltaTime * moveSpeed);
        }

        if (IsKeyDown(turnKeyCode))
        {
            Turn();
        }

        if (IsKeyDown(jumpKeyCode))
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                Jump();
            }
            else
            {
                Turn();
            }
        }

        if(Input.touchCount == 1)
        {
            if(Input.touches[0].position.x > Screen.width / 2)
            {
                Jump();
            }
            else
            {
                Turn();
            }
        }
    }

    void ProcessAutoMove(float distance)
    {
        Vector3 moveVector = Vector3.left * distance;
        Vector3 sideVector = Vector3.zero;
        CheckLinecast("Side", transform.position + moveVector, new Vector3[] { sideCheck[0].position }, out sideVector);
        if (isLeftMove == false)
        {
            moveVector = Vector3.right * distance;
            CheckLinecast("Side",transform.position + moveVector, new Vector3[] { sideCheck[1].position }, out sideVector);
        }

        if (sideVector != Vector3.zero)
        {
            transform.position = sideVector;
            Turn();
            return;
        }

        transform.Translate(moveVector);
    }

    bool IsPossibleToAutoMove()
    {
        return true;
    }

    bool IsKeyDown(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }

    void Turn()
    {
        isLeftMove = !isLeftMove;
    }

    void Jump(bool forceJump = false)
    {
        if (IsCanJump() == false && forceJump == false)
        {
            return;
        }

        isJumping = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
    }

    bool IsCanJump()
    {
        return isGround && IsJumping() == false;
    }

    bool IsJumping()
    {
        return isJumping;
    }

    bool CheckLinecast(string layerName, Vector3 originPos, Vector3[] checkObj, out Vector3 distancePos)
    {
        distancePos = Vector3.zero;
        bool r = false;
        List<RaycastHit2D> coverHitList = new List<RaycastHit2D>();
        foreach(Vector3 obj in checkObj)
        {
            RaycastHit2D[] coverHits;
            coverHits = Physics2D.RaycastAll(originPos, obj, 20f);
            coverHitList.AddRange(coverHits);
        }

        if (coverHitList.Count > 0)
        {
            for (int i = 0; i < coverHitList.Count; i++)
            {
                Debug.DrawLine(originPos, new Vector3(coverHitList[i].transform.position.x, originPos.y, coverHitList[i].transform.position.z), Color.cyan);
                string objLayerName = LayerMask.LayerToName(coverHitList[i].transform.gameObject.layer);
                if (objLayerName == layerName)
                {
                    //float distanceX = Mathf.Abs(coverHitList[i].transform.position.x - transform.position.x);
                    float outboundSizeX = GetComponent<BoxCollider2D>().bounds.extents.x + coverHitList[i].collider.bounds.extents.x;
                    //if(distanceX <= outboundSizeX)

                    Vector3 worldOriginPos = Camera.main.WorldToScreenPoint(originPos);
                    Vector3 worldTargetPos = Camera.main.WorldToScreenPoint(coverHitList[i].transform.position);

                    if (isLeftMove)
                    {
                        if (worldOriginPos.x <= worldTargetPos.x + outboundSizeX)
                        {
                            distancePos = new Vector3(worldTargetPos.x + outboundSizeX, worldOriginPos.y);
                            r = true;
                            break;
                        }
                    }
                    else
                    {
                        if (worldOriginPos.x >= worldTargetPos.x - outboundSizeX)
                        {
                            distancePos = new Vector3(worldTargetPos.x - outboundSizeX, worldOriginPos.y);
                            r = true;
                            break;
                        }
                    }
                }
            }
        }
        return r;
    }

    void CheckGround()
    {
        int layerID = LayerMask.NameToLayer("Ground");
        foreach(Transform vec in groundCheck)
        {
            if(Physics2D.Linecast(transform.position, vec.position, 1 << layerID))
            {
                isGround = true;
                return;
            }
        }

        isGround = false;
    }
}
