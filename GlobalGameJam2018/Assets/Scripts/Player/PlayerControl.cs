using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float acceleration = 500f;
    private float jump_foce = 160.1f;

    private float max_fall_velocity = 8.0f;
    private float max_sides_velocity = 4.0f;

    private Rigidbody2D rigid_body = null;

    private void Awake()
    {
        rigid_body = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start ()
    {
		
	}

    private void Update ()
    {
		if(Input.GetKey("a"))
        {
            MoveLeft();
        }

        if (Input.GetKey("d"))
        {
            MoveRight();
        }

        if (Input.GetKeyDown("w"))
        {
            Jump();
        }

        Cap();
    }

    private void MoveLeft()
    {
        if(rigid_body.velocity.x > -max_sides_velocity)
            rigid_body.AddForce(new Vector2(-acceleration, 0));
    }

    private void MoveRight()
    {
        if(rigid_body.velocity.x < max_sides_velocity)
            rigid_body.AddForce(new Vector2(acceleration, 0));
    }

    private void Jump()
    {
        rigid_body.AddForce(new Vector2(0, jump_foce), ForceMode2D.Impulse);
    }

    private void Cap()
    {
        if(rigid_body.velocity.y < 0 && Mathf.Abs(rigid_body.velocity.y) > max_fall_velocity)
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, -max_fall_velocity);
        }
    }
}
