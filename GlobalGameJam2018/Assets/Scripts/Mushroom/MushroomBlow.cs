using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBlow : MonoBehaviour
{
    [SerializeField] private float max_foce = 100;
    [SerializeField] private float deforce = 10;

    public void SetBlow(GameObject blow)
    {
        Rigidbody2D rb = blow.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float force = max_foce - (Mathf.Abs(Vector3.Distance(gameObject.transform.position, blow.transform.position)) * deforce);

            Vector2 jump = gameObject.transform.up * force;
            rb.AddForce(jump);
        }
    }
}
