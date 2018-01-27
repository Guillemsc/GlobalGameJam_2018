using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject player;

    GameObject curr_level = null;
    GameObject curr_player = null;
    MushroomStar curr_star = null;

    GameObject camera = null;

    int curr_lvl = 0;

    int curr_level_deaths = 0;

    [SerializeField]
    Image star_1;
    [SerializeField]
    Image star_2;
    [SerializeField]
    Image star_3;

    [SerializeField]
    Sprite full_star;
    [SerializeField]
    Sprite half_star;
    [SerializeField]
    Sprite void_star;

    [SerializeField]
    GameObject end_ui;

    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Start()
    {
        LoadLevel(4);
    }

    private void Update()
    {
        // Player dies
        if(curr_player != null && curr_level != null)
        {
            if(curr_player.GetComponent<PlayerControl>().IsDead())
            {
                Vector3 player_spawn = curr_level.GetComponent<Level>().GetSpawn().transform.position;

                curr_player.GetComponent<PlayerControl>().Respawn(player_spawn);

                curr_level_deaths++;
            }
        }

        if(curr_star != null && curr_star.GetFinished())
        {
            end_ui.SetActive(true);
        }
    }

    public void LoadLevel(int level)
    {
        end_ui.SetActive(false);

        for (int i = 0; i < levels.Length; ++i)
        {
            Level curr = levels[i].GetComponent<Level>();

            if(curr.GetLevelNum() == level)
            {
                curr_lvl = level;

                if (curr_level != null)
                {
                    Destroy(curr_level);
                }

                if(player != null)
                {
                    Destroy(curr_player);
                }

                camera.transform.position = new Vector3(curr.GetCameraPos().x, curr.GetCameraPos().y, camera.transform.position.z);

                curr_level = Instantiate(curr.gameObject, new Vector3(0, 0, 0), Quaternion.identity);

                Vector3 player_spawn = curr_level.GetComponent<Level>().GetSpawn().transform.position;
                curr_player = Instantiate(player, player_spawn, Quaternion.identity);

                curr_star = curr.GetStar().GetComponent<MushroomStar>();

                curr_level_deaths = 0;

                break;
            }
        }
    }

    public void NextLevel()
    {
        LoadLevel(curr_lvl++);
    }

    public void ReplayLevel()
    {
        LoadLevel(curr_lvl);
    }
}
