using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject player;

    GameObject curr_level = null;
    GameObject curr_player = null;

    private void Awake()
    {

    }

    private void Start()
    {
        LoadLevel(6);
    }

    private void Update()
    {
        if(curr_player != null && curr_level != null)
        {
            if(curr_player.GetComponent<PlayerControl>().IsDead())
            {
                Vector3 player_spawn = curr_level.GetComponent<Level>().GetSpawn().transform.position;

                curr_player.GetComponent<PlayerControl>().Respawn(player_spawn);
            }
        }
    }

    public void LoadLevel(int level)
    {
        for (int i = 0; i < levels.Length; ++i)
        {
            Level curr = levels[i].GetComponent<Level>();

            if(curr.GetLevelNum() == level)
            {
                if(curr_level != null)
                {
                    Destroy(curr_level);
                }

                if(player != null)
                {
                    Destroy(curr_player);
                }

                curr_level = Instantiate(curr.gameObject, new Vector3(0, 0, 0), Quaternion.identity);

                Vector3 player_spawn = curr_level.GetComponent<Level>().GetSpawn().transform.position;
                curr_player = Instantiate(player, player_spawn, Quaternion.identity);

                break;
            }
        }
    }
}
