using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBlowTestColl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MushroomBlow mp = gameObject.GetComponentInParent<MushroomBlow>();

        if(collision.gameObject.tag == "player")
        {
            collision.gameObject.GetComponent<PlayerControl>().SetCanJump(true);
        }

        if (mp != null)
            mp.SetBlow(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        MushroomBlow mp = gameObject.GetComponentInParent<MushroomBlow>();

        if (mp != null)
            mp.SetBlow(collision.gameObject);
    }
}
