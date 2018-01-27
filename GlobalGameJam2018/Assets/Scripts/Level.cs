using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject player_spawn;
    [SerializeField] private int level_num;
    [SerializeField] private GameObject finish_star;
    [SerializeField] private Vector2 camera_pos;

    public GameObject GetSpawn() { return player_spawn; }
    public int GetLevelNum() { return level_num; }
    public Vector2 GetCameraPos() { return camera_pos; }
    public GameObject GetStar() { return finish_star; }
}


