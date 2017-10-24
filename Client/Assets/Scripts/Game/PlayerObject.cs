﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour {

    bool isJumping = false;
    bool isLeftMove = false;
    float moveSpeed = 6.0f;
    float jumpForce = 2000.0f;
    public Transform groundCheck;

    KeyCode turnKeyCode = KeyCode.Space;
    KeyCode jumpKeyCode = KeyCode.A;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ProcessAutoMove(Time.deltaTime * moveSpeed);

        if (IsKeyDown(turnKeyCode))
        {
            Turn();
        }

        if(IsGrounded())
        {
            isJumping = false;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }

        if(IsKeyDown(jumpKeyCode) && IsCanJump())
        {
            Jump();
        }
	}

    void ProcessAutoMove(float distance)
    {
        Vector3 moveVector = Vector3.left * distance;
        if (isLeftMove == false)
        {
            moveVector = Vector3.right * distance; 
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

    void Jump()
    {
        isJumping = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
    }

    bool IsCanJump()
    {
        return IsGrounded() && IsJumping() == false;
    }

    bool IsJumping()
    {
        return isJumping;
    }

    bool IsGrounded()
    {
        return Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }
}