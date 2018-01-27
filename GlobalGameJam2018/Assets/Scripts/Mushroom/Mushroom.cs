using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public enum MushroomType
    {
        MT_PLATFORM,
        MT_WIND,
    }

    [SerializeField] private MushroomType type;

    private BoxCollider2D collider = null;
    private PlatformEffector2D platform_effector = null;
    private Rigidbody2D rb2d = null;

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

    private MushroomType GetMushroomType()
    {
        return type;
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
