using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]
    Button play;
    [SerializeField]
    Button credits;
    [SerializeField]
    Button credits_back;
    [SerializeField]
    Text credits_text;

    public void Start()
    {
        transform.GetChild(3).gameObject.SetActive(false);
        credits_back.gameObject.SetActive(false);
        credits_text.gameObject.SetActive(false);
    }

    public void OnPlayClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_1");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Title_scene");
    }

    public void OnCreditsClick()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);

        play.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        credits_back.gameObject.SetActive(true);
        credits_text.gameObject.SetActive(true);
    }

    public void OnCreditsBackClick()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);

        play.gameObject.SetActive(true);
        credits.gameObject.SetActive(true);
        credits_back.gameObject.SetActive(false);
        credits_text.gameObject.SetActive(false);
    }
}


