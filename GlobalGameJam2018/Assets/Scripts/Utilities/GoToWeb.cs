using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWeb : MonoBehaviour
{
    public void OpenWeb(string url)
    {
        Application.OpenURL(url);
    }
}
