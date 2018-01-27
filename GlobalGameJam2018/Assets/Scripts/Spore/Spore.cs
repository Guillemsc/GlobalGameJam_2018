﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : MonoBehaviour {

    private Rigidbody2D rigid_body = null;

    [SerializeField] private float max_fall_velocity = 4.0f;

    Timer timer = new Timer();

    [SerializeField] private float life_time = 15.0f;
    
    private void Awake()
    {
        rigid_body = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start ()
    {
        timer.Start();
	}
	
	void Update ()
    {
        Cap();

        if (timer.GetTime() > life_time)
            Destroy(gameObject);
    }

    private void Cap()
    {
        if (rigid_body.velocity.y < 0 && Mathf.Abs(rigid_body.velocity.y) > max_fall_velocity)
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, -max_fall_velocity);
        }
    }
}
