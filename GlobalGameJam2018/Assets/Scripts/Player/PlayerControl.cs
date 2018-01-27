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

    public AudioClip[] audios = null;

    Timer bird_singing = new Timer();
    float random_sing = 0;

    enum audioclips
    {
        bird_1,
        bird_2,
        bird_3,
        bird_4,

        bird_jump
    }

    private AudioSource audio = null;

    private void Awake()
    {
        rigid_body = gameObject.GetComponent<Rigidbody2D>();
        audio = gameObject.GetComponent<AudioSource>();
    }

    private void Start ()
    {
        bird_singing.Start();
        random_sing = Random.Range(3.0f, 10.0f);
	}

    private void FixedUpdate ()
    {
        if (bird_singing.GetTime() > random_sing)
        {
            bird_singing.Start();
            random_sing = Random.Range(3.0f, 10.0f);

            int rand = Random.Range(0, 4);

            audio.clip = audios[rand];
            audio.Play();
        }

        if (Input.GetKey("a"))
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
            audio.clip = audios[(int)audioclips.bird_jump];
            audio.Play();
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
