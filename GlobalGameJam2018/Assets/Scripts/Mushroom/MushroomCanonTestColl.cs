using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCanonTestColl : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MushroomCanon mc = gameObject.GetComponentInParent<MushroomCanon>();

        Debug.Log("hi");

        if (mc != null)
            mc.SetToShoot(collision.gameObject);
    }
}
