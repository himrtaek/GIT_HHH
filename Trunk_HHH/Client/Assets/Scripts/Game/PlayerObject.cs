using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour {

    public bool leftStart = false;

    public float moveSpeed = 6.0f;
    public float jumpForce = 2000.0f;

    public Transform[] groundCheck;
    public Transform[] sideCheck;

    bool isLeftMove = false;
    bool isGround = false;
    bool isJumping = false;
    bool isStop = false;

    float elapsedTime = 0;

    KeyCode turnKeyCode = KeyCode.L;
    KeyCode jumpKeyCode = KeyCode.A;

    void Awake()
    {
        Debug.Log("Width = " + Screen.width.ToString() + ", Height = " + Screen.height.ToString());
        isLeftMove = leftStart;
    }

    void FixedUpdate()
    {
        CheckGround();

        elapsedTime += Time.deltaTime;
        if (elapsedTime < 1)
        {
            return;
        }

        if (isStop == false)
        {
            ProcessAutoMove(Time.deltaTime * moveSpeed);
        }
    }

    void Update()
    {
        if (isGround)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            isJumping = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }

        if (IsKeyDown(turnKeyCode))
        {
            Turn();
        }

        if (IsKeyDown(jumpKeyCode))
        {
            Jump();
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition.ToString());
            if (Input.mousePosition.x > Screen.width / 2)
            {
                Jump();
            }
            else
            {
                Turn();
            }
        }
#endif


        if(Input.touchCount > 0 && 
           Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Began)
        {
            Debug.Log(Input.GetTouch(Input.touchCount - 1).position.ToString());
            if (Input.GetTouch(Input.touchCount - 1).position.x > Screen.width / 2)
            {
                Jump();
            }
            else if (Input.GetTouch(Input.touchCount - 1).position.x <= Screen.width / 2)
            {
                Turn();
            }
        }
    }

    void ProcessAutoMove(float distance)
    {
        Vector3 moveVector = Vector3.zero;
        Vector3 sideVector = Vector3.zero;

        if (isLeftMove)
        {
            moveVector = Vector3.left * distance;
            CheckLinecast("Side", transform.position, new Vector3[] { sideCheck[0].position}, out sideVector);
        }
        else
        {
            moveVector = Vector3.right * distance;
            CheckLinecast("Side", transform.position, new Vector3[] { sideCheck[1].position}, out sideVector);
        }

        if (sideVector != Vector3.zero)
        {
            transform.position = new Vector3(sideVector.x, transform.position.y, transform.position.y);
            Turn();
            ProcessAutoMove(Time.deltaTime * moveSpeed);
            return;
        }

        transform.Translate(moveVector);
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
            float fRaycastDistance = originPos.x < obj.x ? 3f : -3f;
            coverHits = Physics2D.RaycastAll(new Vector3(originPos.x + fRaycastDistance, originPos.y, originPos.z), obj, fRaycastDistance);
            coverHitList.AddRange(coverHits);
        }

        if (coverHitList.Count > 0)
        {
            for (int i = 0; i < coverHitList.Count; i++)
            {
                string objLayerName = LayerMask.LayerToName(coverHitList[i].transform.gameObject.layer);
                if (objLayerName == layerName)
                {
                    Debug.DrawLine(originPos, new Vector3(coverHitList[i].transform.position.x, originPos.y, coverHitList[i].transform.position.z), Color.cyan);
                    float outboundSizeX = GetComponent<BoxCollider2D>().bounds.extents.x + coverHitList[i].collider.bounds.extents.x;

                    Vector3 worldOriginPos = originPos;
                    Vector3 worldTargetPos = coverHitList[i].transform.position;

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

public static class DrawArrow
{
    public static void ForGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 0.5f)
    {
        ForGizmo(pos, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle, arrowPosition);
    }

    public static void ForGizmo(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 0.5f)
    {
        Gizmos.color = color;
        Gizmos.DrawRay(pos, direction);
        DrawArrowEnd(true, pos, direction, color, arrowHeadLength, arrowHeadAngle, arrowPosition);
    }

    public static void ForDebug(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 0.5f)
    {
        ForDebug(pos, direction, Color.white, arrowHeadLength, arrowHeadAngle, arrowPosition);
    }

    public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 0.5f)
    {
        Debug.DrawRay(pos, direction, color);
        DrawArrowEnd(false, pos, direction, color, arrowHeadLength, arrowHeadAngle, arrowPosition);
    }
    private static void DrawArrowEnd(bool gizmos, Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 0.5f)
    {
        Vector3 right = (Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength;
        Vector3 left = (Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength;
        Vector3 up = (Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength;
        Vector3 down = (Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength;

        Vector3 arrowTip = pos + (direction * arrowPosition);

        if (gizmos)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(arrowTip, right);
            Gizmos.DrawRay(arrowTip, left);
            Gizmos.DrawRay(arrowTip, up);
            Gizmos.DrawRay(arrowTip, down);
        }
        else
        {
            Debug.DrawRay(arrowTip, right, color);
            Debug.DrawRay(arrowTip, left, color);
            Debug.DrawRay(arrowTip, up, color);
            Debug.DrawRay(arrowTip, down, color);
        }
    }
}