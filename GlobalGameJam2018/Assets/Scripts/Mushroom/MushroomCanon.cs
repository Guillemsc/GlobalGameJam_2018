using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCanon : MonoBehaviour
{
    public enum MushroomCanonState
    {
        MC_ROTATE,
        MC_SUCK,
        MC_SHOOT,
    }

    public enum MushromCanonDirection
    {
        MC_LEFT,
        MS_RIGHT,
    }

    Mushroom mush = null;
    GameObject spore = null;

    [SerializeField] float shoot_force;
    [SerializeField] float rotation_speed = 0.2f;
    private float ia_rotation_speed = 0.0f;

    float suck_time = 5;
    float rotation_time = 2;
    float shoot_time = 1;

    Timer suck_timer = new Timer();
    Timer rotation_timer = new Timer();
    Timer shoot_timer = new Timer();

    [SerializeField] GameObject canon_pivot;

    MushroomCanonState state = MushroomCanonState.MC_SUCK;
    MushromCanonDirection rotation_dir = MushromCanonDirection.MS_RIGHT;

    private GameObject to_shoot = null;

    private void Awake()
    {
        mush = gameObject.GetComponent<Mushroom>();
    }

    private void Start()
    {
        suck_timer.Start();
        state = MushroomCanonState.MC_SUCK;
        rotation_dir = GetRandomRotDir();
    }

    private void Update()
    {
        if (suck_timer.GetTime() > suck_time && state == MushroomCanonState.MC_SUCK)
        {
            rotation_timer.Start();
            state = MushroomCanonState.MC_ROTATE;

            rotation_dir = GetRandomRotDir();
            ia_rotation_speed = Random.Range(0.1f, 0.5f);
        }

        if(rotation_timer.GetTime() > rotation_time && state == MushroomCanonState.MC_ROTATE)
        {
            //Shoot();

            shoot_timer.Start();
            state = MushroomCanonState.MC_SHOOT;
        }

        if(shoot_timer.GetTime() > shoot_time && state == MushroomCanonState.MC_SHOOT)
        {
            suck_timer.Start();
            state = MushroomCanonState.MC_SUCK;
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case MushroomCanonState.MC_SUCK:
                break;

            case MushroomCanonState.MC_ROTATE:
                if(to_shoot != null && to_shoot.tag == "player")
                {
                    Rotate();
                }
                else
                {
                    RotateIA();
                }
                break;

            case MushroomCanonState.MC_SHOOT:
                break;
        }
    }

    private void RotateIA()
    {
        if (rotation_dir == MushromCanonDirection.MC_LEFT)
        {
            canon_pivot.transform.rotation
                = Quaternion.Euler(canon_pivot.transform.rotation.eulerAngles.x,
                canon_pivot.transform.rotation.eulerAngles.y,
                canon_pivot.transform.rotation.eulerAngles.z + ia_rotation_speed);
        }
        else
        {
            canon_pivot.transform.rotation
             = Quaternion.Euler(canon_pivot.transform.rotation.eulerAngles.x,
             canon_pivot.transform.rotation.eulerAngles.y,
             canon_pivot.transform.rotation.eulerAngles.z - ia_rotation_speed);

        }
    }

    private void Rotate()
    {
        if (Input.GetKey("a"))
        {
            canon_pivot.transform.rotation
                = Quaternion.Euler(canon_pivot.transform.rotation.eulerAngles.x,
                canon_pivot.transform.rotation.eulerAngles.y,
                canon_pivot.transform.rotation.eulerAngles.z + ia_rotation_speed);
        }

        if (Input.GetKey("d"))
        {
            canon_pivot.transform.rotation
                = Quaternion.Euler(canon_pivot.transform.rotation.eulerAngles.x,
                canon_pivot.transform.rotation.eulerAngles.y,
                canon_pivot.transform.rotation.eulerAngles.z - ia_rotation_speed);
        }
    }


    private void Shoot()
    {
        Rigidbody2D rb = null;

        if (to_shoot == null && spore != null)
        {
            rb = spore.GetComponent<Rigidbody2D>();
        }
        else
        {
            rb = to_shoot.GetComponent<Rigidbody2D>();
        }

        if(rb != null)
        {
            rb.AddForce(canon_pivot.transform.up * shoot_force);
        }
    }

    public void SetToShoot(GameObject go)
    {
        if(to_shoot == null && state == MushroomCanonState.MC_SUCK)
        {
            to_shoot = go;
        }
    }

    private MushromCanonDirection GetRandomRotDir()
    {
        MushromCanonDirection ret = MushromCanonDirection.MC_LEFT;

        int rand = Random.Range(0, 2);

        if(rand == 0)
        {
            ret = MushromCanonDirection.MC_LEFT;
        }
        else
        {
            ret = MushromCanonDirection.MS_RIGHT;
        }

        if (canon_pivot.transform.rotation.eulerAngles.z > 270)
        {
            ret = MushromCanonDirection.MC_LEFT;
        }
        else if (canon_pivot.transform.rotation.eulerAngles.z > 45)
        {
            ret = MushromCanonDirection.MS_RIGHT;
        }

        return ret;
    }
}
