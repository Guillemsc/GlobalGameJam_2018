using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCanonTestColl : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MushroomCanon mc = gameObject.GetComponentInParent<MushroomCanon>();

        if (mc != null)
            mc.SetToShoot(collision.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        MushroomCanon mc = gameObject.GetComponentInParent<MushroomCanon>();

        if (mc != null)
            mc.SetToShoot(other.gameObject);
    }
}
