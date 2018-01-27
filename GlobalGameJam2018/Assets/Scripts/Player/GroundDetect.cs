using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<PlayerControl>().GroundCollider(collision);
    }
}
