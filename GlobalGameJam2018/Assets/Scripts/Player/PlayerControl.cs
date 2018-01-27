using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float acceleration = 500f;
    private float jump_foce = 70.1f;

    private float max_fall_velocity = 8.0f;
    private float max_sides_velocity = 1.5f;

    private bool can_jump = false;

    private Rigidbody2D rigid_body = null;

    private void Awake()
    {
        rigid_body = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start ()
    {
		
	}

    private void FixedUpdate ()
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

    public void Disappear()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void Appear()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
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
        if (can_jump)
        {
            rigid_body.AddForce(new Vector2(0, jump_foce), ForceMode2D.Impulse);
            can_jump = false;
        }
    }

    private void Cap()
    {
        if(rigid_body.velocity.y < 0 && Mathf.Abs(rigid_body.velocity.y) > max_fall_velocity)
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, -max_fall_velocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "platform")
        {
            can_jump = true;
        }
    }
}
