using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPlatformTestColl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MushroomPlatform mp = gameObject.GetComponentInParent<MushroomPlatform>();

        if (collision.tag == "ground_detect")
        {
            if (mp != null)
                mp.SetCheck(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MushroomPlatform mp = gameObject.GetComponentInParent<MushroomPlatform>();

        if (collision.tag == "ground_detect")
        {
            if (mp != null)
                mp.SetCheck(false);
        }
    }
}
