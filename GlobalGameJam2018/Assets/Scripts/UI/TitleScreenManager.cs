using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]
    GameObject title;
    [SerializeField]
    Button play;
    [SerializeField]
    Button credits;
    [SerializeField]
    GameObject credits_group;

    public void Start()
    {
        Menu();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void OnPlayClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_1");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Title_scene");
    }

    private void Menu()
    {
        title.SetActive(true);
        credits_group.SetActive(false);
        play.gameObject.SetActive(true);
        credits.gameObject.SetActive(true);
    }

    private void Credits()
    {
        title.SetActive(false);
        credits_group.SetActive(true);
        play.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
    }

    public void OnCreditsClick()
    {
        Credits();
    }

    public void OnCreditsBackClick()
    {
        Menu();
    }
}


