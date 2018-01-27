using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{

    public void OnPlayClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_1");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Title_scene");
    }
}


