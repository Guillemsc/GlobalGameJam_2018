using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBlowTestColl : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D other)
    {
        MushroomBlow mp = gameObject.GetComponentInParent<MushroomBlow>();

        if (mp != null)
            mp.SetBlow(other.gameObject);
    }
}
