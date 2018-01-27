using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBlow : MonoBehaviour
{
    [SerializeField] private float max_foce = 100;
    [SerializeField] private float deforce = 10;

    [SerializeField] private float blow_time = 6.0f;
    [SerializeField] private float recharge_time = 2.0f;

    [SerializeField] private float spore_force = 0.4f;

    private bool shot_spore = false;

    private Timer timer = new Timer();

    private Animator anim;

    enum States
    {
        S_BLOWING,
        S_RECHARGE,
    }

    [SerializeField]
    States curr_state = States.S_RECHARGE;

    private AudioSource audio = null;

    public void SetBlow(GameObject blow)
    {
        if (curr_state == States.S_BLOWING)
        {
            Rigidbody2D rb = blow.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;

                float force = max_foce - (Mathf.Abs(Vector3.Distance(gameObject.transform.position, blow.transform.position)) * deforce);

                Vector2 jump = gameObject.transform.up * force;
                rb.AddForce(jump);
            }
        }
    }

    private void Awake()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    public void Start()
    {
        timer.Start();
        anim = GetComponent<Animator>();
        anim.SetBool("blowing", true);
    }

    public void Update()
    {
        CheckState();

        if(curr_state == States.S_BLOWING)
        {
            if (timer.GetTime() > blow_time - 0.9f && shot_spore)
                anim.SetBool("shot", true);

            if (timer.GetTime() > blow_time - 0.5f && shot_spore)
            {
                shot_spore = false;
                Vector3 spore_pos = transform.position;
                spore_pos.y += 0.1f;
                GameObject spore = Instantiate(GetComponent<Mushroom>().spore_prefab,spore_pos,Quaternion.identity);
                int rand_direction = Random.Range(0, 2);
                Vector2 direction;
                if (rand_direction == 0)
                    direction = -gameObject.transform.right;
                else direction = gameObject.transform.right;
                Vector2 force = direction * spore_force * Random.Range(0.9f,1.4f);
                spore.GetComponent<Rigidbody2D>().AddForce(force);
                audio.Play();
                
            }
        }
        else
        {
            if (timer.GetTime() > recharge_time - 0.9)
                anim.SetBool("blowing", true);
        }

    }

    void CheckState()
    {
        switch (curr_state)
        {
            case States.S_BLOWING:
                if (timer.GetTime() > blow_time)
                {
                    curr_state = States.S_RECHARGE;
                    timer.Start();
                    anim.SetBool("blowing", false);
                    anim.SetBool("shot", false);
                }
                break;
            case States.S_RECHARGE:
                if(timer.GetTime() > recharge_time)
                {
                    curr_state = States.S_BLOWING;
                    timer.Start();
                    shot_spore = true;
                }
                break;
            default:
                break;
        }
    }
}
