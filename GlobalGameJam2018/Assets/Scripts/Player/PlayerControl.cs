using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float speed = 0.1f;
    private float jump_foce = 10.1f;

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
    }

    private void MoveLeft()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - speed, gameObject.transform.position.y);
    }

    private void MoveRight()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + speed, gameObject.transform.position.y);
    }

    private void Jump()
    {
        rigid_body.AddForce(new Vector2(0, jump_foce), ForceMode2D.Impulse);
    }
}
