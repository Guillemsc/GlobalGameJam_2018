using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomStarTestColl : MonoBehaviour {

    MushroomStar ms = null;

	// Use this for initialization
	void Start () {
        ms = gameObject.GetComponentInParent<MushroomStar>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ms.setCheck(true);
    }
}
