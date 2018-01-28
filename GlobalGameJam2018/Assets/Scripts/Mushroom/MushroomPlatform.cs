using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPlatform : MonoBehaviour
{
    [SerializeField] private float force;

    private BoxCollider2D base_coll = null;

    [SerializeField] private bool check = false;

    private void Awake()
    {
        base_coll = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

    }

    public void SetCheck(bool _check)
    {
        check = _check;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null && check)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);

            Vector2 jump = gameObject.transform.up.normalized * force;
            rb.AddForce(jump, ForceMode2D.Force);

            check = false;
        }
    }
}
