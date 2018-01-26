using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    enum PlatformColissionSide
    {
        CS_UP,
        CS_SIDE,
        CS_DOWN,
        CS_NONE,
    }


    [SerializeField] private PlatformColissionSide coll_side = PlatformColissionSide.CS_NONE;
    [SerializeField] private bool acting = false;

    private BoxCollider2D collider = null;

    private GameObject player = null;
    private CapsuleCollider2D player_collider = null;
    private GameObject player_base = null;

    [SerializeField] float distance_to_player = 0.0f;

    private float distance_rays = 10;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        player_collider = player.GetComponent<CapsuleCollider2D>();
        player_base = GameObject.FindGameObjectWithTag("player_base");
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Vector2 plat_size = CalculatePlatformSize();

        float distance = 0;
        if(plat_size.x > plat_size.y)
            distance = plat_size.x;
        else
            distance = plat_size.y;

        distance_to_player = Mathf.Abs(Vector3.Distance(transform.position, player.transform.position));

        if (distance_to_player < distance*2)
        {
            acting = true;

            coll_side = DetectCollisionSide();

            if (coll_side == PlatformColissionSide.CS_UP)
                collider.enabled = true;
            else
                collider.enabled = false;
        }
        else
        {
            acting = false;

            collider.enabled = false;
            coll_side = PlatformColissionSide.CS_NONE;
        }
    }

    private Vector3[] CalculateVertices()
    {
        Vector3[] ret = new Vector3[4];

        float top = collider.offset.y + (collider.size.y / 2f);
        float btm = collider.offset.y - (collider.size.y / 2f);
        float left = collider.offset.x - (collider.size.x / 2f);
        float right = collider.offset.x + (collider.size.x / 2f);

        ret[0] = transform.TransformPoint(new Vector3(left, top, 0f));
        ret[3] = transform.TransformPoint(new Vector3(right, top, 0f));
        ret[1] = transform.TransformPoint(new Vector3(left, btm, 0f));
        ret[2] = transform.TransformPoint(new Vector3(right, btm, 0f));

        return ret;
    }

    private Vector2 CalculatePlayerSize()
    {
        Vector2 ret = new Vector2(0, 0);

        float top = player_collider.offset.y + (player_collider.size.y / 2f);
        float btm = player_collider.offset.y - (player_collider.size.y / 2f);
        float left = player_collider.offset.x - (player_collider.size.x / 2f);
        float right = player_collider.offset.x + (player_collider.size.x / 2f);

        Vector3 topLeft = player.transform.TransformPoint(new Vector3(left, top, 0f));
        Vector3 topRight = player.transform.TransformPoint(new Vector3(right, top, 0f));
        Vector3 btmLeft = player.transform.TransformPoint(new Vector3(left, btm, 0f));
        Vector3 btmRight = player.transform.TransformPoint(new Vector3(right, btm, 0f));

        ret.x = btmRight.x - btmLeft.x;
        ret.y = topLeft.y - btmLeft.y;

        return ret;
    }

    private Vector2 CalculatePlatformSize()
    {
        Vector2 ret = new Vector2(0, 0);

        float top = collider.offset.y + (collider.size.y / 2f);
        float btm = collider.offset.y - (collider.size.y / 2f);
        float left = collider.offset.x - (collider.size.x / 2f);
        float right = collider.offset.x + (collider.size.x / 2f);

        Vector3 topLeft = transform.TransformPoint(new Vector3(left, top, 0f));
        Vector3 topRight = transform.TransformPoint(new Vector3(right, top, 0f));
        Vector3 btmLeft = transform.TransformPoint(new Vector3(left, btm, 0f));
        Vector3 btmRight = transform.TransformPoint(new Vector3(right, btm, 0f));

        ret.x = btmRight.x - btmLeft.x;
        ret.y = topLeft.y - btmLeft.y;

        return ret;
    }

    private PlatformColissionSide DetectCollisionSide()
    {
        PlatformColissionSide ret = PlatformColissionSide.CS_NONE;

        Vector3[] vertices = CalculateVertices();
        Vector2 player_size = CalculatePlayerSize();

        LayerMask player_mask = (1 << player.layer);
        LayerMask player_base_mask = (1 << player_base.layer);

        RaycastHit2D hit;

        // UP ---------------------------------
        Vector3 curr_ray_pos = vertices[0];

        while(curr_ray_pos.x < vertices[3].x)
        {
            hit = Physics2D.Raycast(curr_ray_pos, Vector2.up, distance_rays, player_mask);

            if(hit.collider != null)
            {
                return PlatformColissionSide.CS_UP;
            }

            curr_ray_pos.x += player_size.x;
        }

        curr_ray_pos = vertices[3];

        hit = Physics2D.Raycast(curr_ray_pos, Vector2.up, distance_rays, player_mask);

        if (hit.collider != null)
        {
            return PlatformColissionSide.CS_UP;
        }

        // ------------------------------------

        // DOWN -------------------------------

        curr_ray_pos = vertices[1];

        while (curr_ray_pos.x < vertices[2].x)
        {
            hit = Physics2D.Raycast(curr_ray_pos, Vector2.down, distance_rays, player_mask);

            if (hit.collider != null)
            {
                return PlatformColissionSide.CS_DOWN;
            }

            curr_ray_pos.x += player_size.x;
        }

        curr_ray_pos = vertices[2];

        hit = Physics2D.Raycast(curr_ray_pos, Vector2.down, distance_rays, player_mask);

        if (hit.collider != null)
        {
            return PlatformColissionSide.CS_DOWN;
        }

        // ------------------------------------

        // LEFT -------------------------------

        curr_ray_pos = vertices[0];

        while (curr_ray_pos.y > vertices[1].y)
        {
            hit = Physics2D.Raycast(curr_ray_pos, Vector2.left, distance_rays, player_mask);

            if (hit.collider != null)
            {
                return PlatformColissionSide.CS_SIDE;
            }

            curr_ray_pos.y -= player_size.y;
        }

        curr_ray_pos = vertices[1];

        hit = Physics2D.Raycast(curr_ray_pos, Vector2.left, distance_rays, player_mask);

        if (hit.collider != null)
        {
            return PlatformColissionSide.CS_SIDE;
        }

        // ------------------------------------

        // RIGHT ------------------------------

        curr_ray_pos = vertices[3];

        while (curr_ray_pos.y > vertices[2].y)
        {
            hit = Physics2D.Raycast(curr_ray_pos, Vector2.right, distance_rays, player_mask);

            if (hit.collider != null)
            {
                return PlatformColissionSide.CS_SIDE;
            }

            curr_ray_pos.y -= player_size.y;
        }

        curr_ray_pos = vertices[2];

        hit = Physics2D.Raycast(curr_ray_pos, Vector2.right, distance_rays, player_mask);

        if (hit.collider != null)
        {
            return PlatformColissionSide.CS_SIDE;
        }

        // ------------------------------------

        return ret;
    }
}
