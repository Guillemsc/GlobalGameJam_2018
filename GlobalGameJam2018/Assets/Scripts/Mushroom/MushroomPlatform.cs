using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        GameObject go = collision.gameObject;

        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

        if(rb != null && check)
        {
            Vector2 jump = gameObject.transform.up * force;
            rb.AddForce(jump);

            Debug.Log(jump);
        }
    }
}
