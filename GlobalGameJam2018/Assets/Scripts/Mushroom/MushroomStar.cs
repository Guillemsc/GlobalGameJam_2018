using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomStar : MonoBehaviour {

    private bool check = false;
    private AudioSource audio = null;
    private bool finished = false;

	// Use this for initialization
	void Awake () {
        audio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (check)
        {
            audio.Play();
            check = false;
        }
	}

    public void setCheck(bool _check)
    {
        if (check != true)
        {
            check = _check;
            finished = true;
        }
    }

    public bool GetFinished() { return finished; }
}
