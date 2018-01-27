using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float acceleration = 500f;
    [SerializeField] private float jump_foce = 70.1f;
    [SerializeField] private float break_force = 200.0f;

    [SerializeField] private float max_fall_velocity = 4.0f;
    [SerializeField] private float max_sides_velocity = 1.5f;
    [SerializeField] private float max_jump_sides_velocity = 1.5f;

    private bool can_jump = false;

    private Rigidbody2D rigid_body = null;
    private SpriteRenderer sprite_ren = null;

    public AudioClip[] audios = null;

    Timer bird_singing = new Timer();
    float random_sing = 0;

    bool mushroom_in_head = false;
    Mushroom.MushroomType type_in_head = Mushroom.MushroomType.MT_CANON;
    Timer alive_in_head = new Timer();
    [SerializeField] float time_alive_in_head = 5;

    [SerializeField] GameObject blow_prefab;
    [SerializeField] GameObject canon_prefab;
    [SerializeField] GameObject platform_prefab;

    GameObject player_mushroom = null;

    public bool alive = true;

    enum audioclips
    {
        bird_1,
        bird_2,
        bird_3,
        bird_4,

        bird_jump
    }

    private AudioSource audio = null;
    private Animator animator = null;

    private void Awake()
    {
        rigid_body = gameObject.GetComponent<Rigidbody2D>();
        audio = gameObject.GetComponent<AudioSource>();
        player_mushroom = GameObject.FindGameObjectWithTag("player_mushroom");
        animator = gameObject.GetComponentInChildren<Animator>();
        sprite_ren = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void Start ()
    {
        bird_singing.Start();
        random_sing = Random.Range(3.0f, 10.0f);
	}

    private void Update()
    {
        if (mushroom_in_head)
        {
            if (alive_in_head.GetTime() > time_alive_in_head)
            {
                InstantiateMush();
                Disappear();

                mushroom_in_head = false;
                alive = false;
            }
        }

        if (alive)
        {
            if (bird_singing.GetTime() > random_sing)
            {
                bird_singing.Start();
                random_sing = Random.Range(3.0f, 10.0f);

                int rand = Random.Range(0, 4);

                audio.clip = audios[rand];
                audio.Play();
            }

            if (Input.GetKeyDown("w"))
            {
                Jump();
            }

            if (Input.GetKeyDown("j"))
            {
                LookForMushroom();
            }

            animator.SetFloat("velocity_x_abs", Mathf.Abs(rigid_body.velocity.x));
        }
    }

    private void FixedUpdate ()
    {
        if(alive)
        {
            if (Input.GetKey("a"))
            {
                MoveLeft();

                sprite_ren.flipX = true;
            }
            else if(rigid_body.velocity.x < -1)
            {
                rigid_body.AddForce(new Vector2(break_force, 0));
            }

            if (Input.GetKey("d"))
            {
                MoveRight();

                sprite_ren.flipX = false;
            }
            else if (rigid_body.velocity.x > 1)
            {
                rigid_body.AddForce(new Vector2(-break_force, 0));
            }

            Cap();
        }
    }

    public void Disappear()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        player_mushroom.SetActive(false);
    }

    public void Appear()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        player_mushroom.SetActive(true);
    }

    public void Kill()
    {
        Disappear();
        alive = false;
    }

    public void Respawn(Vector3 pos)
    {
        Appear();
        alive = true;
        gameObject.transform.position = pos;
    }

    public bool IsDead() { return !alive; }

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
            audio.clip = audios[4];
            audio.Play();

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

        if(rigid_body.velocity.y > 0)
        {
            if (Mathf.Abs(rigid_body.velocity.x) > max_jump_sides_velocity)
            {
                if (rigid_body.velocity.x > 0)
                    rigid_body.velocity = new Vector2(max_jump_sides_velocity, rigid_body.velocity.y);

                if (rigid_body.velocity.x < 0)
                    rigid_body.velocity = new Vector2(-max_jump_sides_velocity, rigid_body.velocity.y);
            }
        }
    }

    private void LookForMushroom()
    {
        if (!mushroom_in_head)
        {
            GameObject[] mushrooms = GameObject.FindGameObjectsWithTag("mushroom");

            GameObject closest = null;
            float closest_dist = 9999999;
            for (int i = 0; i < mushrooms.Length; ++i)
            {
                float dist = Vector3.Distance(gameObject.transform.position, mushrooms[i].transform.position);
                if (dist < closest_dist)
                {
                    closest_dist = dist;
                    closest = mushrooms[i];
                }
            }

            if (closest != null && Vector3.Distance(gameObject.transform.position, closest.transform.position) < 2)
            {
                mushroom_in_head = true;
                type_in_head = closest.GetComponent<Mushroom>().GetMushroomType();
                alive_in_head.Start();

                switch (type_in_head)
                {
                    case Mushroom.MushroomType.MT_PLATFORM:
                        player_mushroom.GetComponent<Animator>().SetBool("platform", true);
                        break;
                    case Mushroom.MushroomType.MT_WIND:
                        player_mushroom.GetComponent<Animator>().SetBool("blow", true);
                        break;
                    case Mushroom.MushroomType.MT_CANON:
                        player_mushroom.GetComponent<Animator>().SetBool("canon", true);
                        break;
                    default:
                        break;
                }
            }

            animator.SetBool("peck", true);
        }
    }

    private void InstantiateMush()
    {
        Vector3 pos = player_mushroom.transform.position;

        switch (type_in_head)
        {
            case Mushroom.MushroomType.MT_CANON:
                Instantiate(canon_prefab, gameObject.transform.position, Quaternion.identity);
                break;
            case Mushroom.MushroomType.MT_PLATFORM:
                Instantiate(platform_prefab, gameObject.transform.position, Quaternion.identity);
                break;
            case Mushroom.MushroomType.MT_WIND:
                Instantiate(blow_prefab, gameObject.transform.position, Quaternion.identity);
                break;

        }
    }

    public void GroundCollider(Collider2D collision)
    {
        if (collision.gameObject.tag == "platform" || collision.gameObject.tag == "mushroom")
        {
            can_jump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "spore")
        {
            Kill();
        }
    }
}
