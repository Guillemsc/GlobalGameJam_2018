using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public enum MushroomType
    {
        MT_PLATFORM,
        MT_WIND,
        MT_CANON,
        MT_STAR,
    }

    [SerializeField] private MushroomType type;

    private BoxCollider2D collider = null;
    private PlatformEffector2D platform_effector = null;
    private Rigidbody2D rb2d = null;

    [SerializeField] private float max_fall_vel = 0.3f;
    [SerializeField] private float max_x_velocity = 5;

    public GameObject spore_prefab;

    private void Awake()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        platform_effector = gameObject.GetComponent<PlatformEffector2D>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        SetDynamic();
    }

    private void Start()
    {
        SetDynamic();
    }

    private void Update()
    {
        if(rb2d.velocity.y > 0)
            SetDynamic();

        Cap();
    }

    public MushroomType GetMushroomType()
    {
        return type;
    }

    private void Cap()
    {
        if (rb2d.velocity.y < 0 && Mathf.Abs(rb2d.velocity.y) > max_fall_vel)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -max_fall_vel);
        }

        if (Mathf.Abs(rb2d.velocity.x) > max_x_velocity && rb2d.velocity.y < 0)
        {
            if (rb2d.velocity.x > 0)
                rb2d.velocity = new Vector2(max_x_velocity, rb2d.velocity.y);
     
            if (rb2d.velocity.x < 0)
                rb2d.velocity = new Vector2(-max_x_velocity, rb2d.velocity.y);
        }
    }

    private void SetDynamic()
    {
        platform_effector.enabled = false;
        collider.usedByEffector = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private void SetStatic()
    {
        platform_effector.enabled = true;
        collider.usedByEffector = true;
        rb2d.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "platform")
        {
            SetStatic();
        }
    }
}
