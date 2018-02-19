using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform[] Sides;
    public GameObject player;
    private float fStartYPos;

    private void Start()
    {
        fStartYPos = transform.position.y;
    }

    void LateUpdate()
    {   
        float fDistance = transform.position.y - player.transform.position.y;
        if (Mathf.Abs(fDistance) > 30)
        {
            if (fDistance >= 0)
            {
                if (transform.position.y < fStartYPos)
                {
                    return;
                }

                Move(Vector3.down * Mathf.Abs(fDistance) * 0.02f);
            }
            else
            {
                Move(Vector3.up * Mathf.Abs(fDistance) * 0.02f);
            }
        }
    }

    void Move(Vector3 moveVector)
    {
        if(moveVector.y < 0 && transform.position.y + moveVector.y < fStartYPos)
        {
            moveVector.y = fStartYPos - transform.position.y;
        }

        transform.Translate(moveVector);

        foreach(var trn in Sides)
        {
            trn.Translate(moveVector);
        }
    }
}