using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizatedText : MonoBehaviour
{
    [SerializeField] private string key;

    private LocalizationManager localization = null;
    private Text text;

    private void Awake()
    {
        localization = GameObject.FindGameObjectWithTag("dialog_manager").GetComponent<LocalizationManager>();
        text = gameObject.GetComponent<Text>();
        localization.AddUIText(this);
    }

    void Start()
    {
        SetText();
    }

    public void SetText()
    {
        text.text = localization.GetText(key);
    }

    private void OnDestroy()
    {
        localization.RemoveUIText(this);
    }
}