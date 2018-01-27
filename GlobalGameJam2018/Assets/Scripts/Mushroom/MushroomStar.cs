using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomStar : MonoBehaviour
{

    private bool check = false;
    private AudioSource audio = null;
    private bool finished = false;

	void Awake ()
    {
        audio = gameObject.GetComponent<AudioSource>();
	}
	
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
        if(check != true)
            check = _check;

        GameObject.FindGameObjectWithTag("game_manager").GetComponent<GameManager>().LevelFinished();    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
            setCheck(true);
    }

    public bool GetFinished() { return finished; }
}
