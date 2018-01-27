using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : MonoBehaviour {

    private Rigidbody2D rigid_body = null;

    [SerializeField] private float max_fall_velocity = 4.0f;
    [SerializeField] private float max_x_velocity = 1.0f;

    
    private void Awake()
    {
        rigid_body = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        Cap();
    }

    private void Cap()
    {
        if (rigid_body.velocity.y < 0 && Mathf.Abs(rigid_body.velocity.y) > max_fall_velocity)
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, -max_fall_velocity);
        }

        if(Mathf.Abs(rigid_body.velocity.x) > max_x_velocity)
        {
            if(rigid_body.velocity.x > 0)
                rigid_body.velocity = new Vector2(max_x_velocity, rigid_body.velocity.y);

            if (rigid_body.velocity.x < 0)
                rigid_body.velocity = new Vector2(-max_x_velocity, rigid_body.velocity.y);
        }
    }
}
