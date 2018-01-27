using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject player_spawn;
    [SerializeField] private int level_num;

    public GameObject GetSpawn() { return player_spawn; }
    public int GetLevelNum() { return level_num; }
}


