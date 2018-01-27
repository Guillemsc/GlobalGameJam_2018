using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeOscillation : MonoBehaviour {

    [SerializeField]
    private float oscillation = 1.0f;

    private float oscillation_x;

    [SerializeField]
    private float phase_increment = 5.0f;
    private float phase = 0;

    private Rigidbody2D rigid_body = null;

    private void Awake()
    {
        rigid_body = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (rigid_body.velocity.y < 0)
        {
            oscillation_x = transform.parent.position.x;

            Vector3 new_pos = transform.position;

            new_pos.x = oscillation_x + Mathf.Sin(phase) * oscillation;
            phase += phase_increment * Time.deltaTime;

            transform.position = new_pos;
        }
    }
}
