using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject player_spawn;
    [SerializeField] private int level_num;
    [SerializeField] private GameObject finish_star;
    [SerializeField] private Vector2 camera_pos;

    [SerializeField] private int three_stars_deaths = 1;
    [SerializeField] private int two_and_half_stars_deaths = 2;
    [SerializeField] private int two_stars_deaths = 3;
    [SerializeField] private int one_and_half_stars_deaths = 4;
    [SerializeField] private int one_star_deaths = 5;
    [SerializeField] private int half_star_deaths = 6;

    public GameObject GetSpawn() { return player_spawn; }
    public int GetLevelNum() { return level_num; }
    public Vector2 GetCameraPos() { return camera_pos; }
    public GameObject GetStar() { return finish_star; }

    public int CheckStars(int deaths)
    {
        if (deaths <= three_stars_deaths) return 6;
        else if (deaths <= two_and_half_stars_deaths) return 5;
        else if (deaths <= two_stars_deaths) return 4;
        else if (deaths <= one_and_half_stars_deaths) return 3;
        else if (deaths <= one_star_deaths) return 2;
        else if (deaths <= half_star_deaths) return 1;
        return 0;
    }
}


