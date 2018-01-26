using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public enum Language
    {
        EN,
        SPA,
        CAT,
    }

    private IDictionary<string, string> _content = new Dictionary<string, string>();

    private Language language = Language.EN;

    string path;

    List<LocalizatedText> texts = new List<LocalizatedText>();

    public void Awake()
    {
        SetLanguage(Language.CAT);
    }

    void OnGUI()
    {


    }

    public void SetLanguage(Language _language)
    {
        language = _language;

        LoadData(_language);

        for (int i = 0; i < texts.Count; ++i)
        {
            texts[i].SetText();
        }
    }

    public Language GetLanguage()
    {
        return language;
    }

    public string GetText(string key)
    {
        string ret = string.Empty;

        _content.TryGetValue(key, out ret);

        if (string.IsNullOrEmpty(ret))
            ret = key + "[" + language.ToString() + "]" + " No Text defined";

        return ret;
    }

    public void AddUIText(LocalizatedText text)
    {
        texts.Add(text);
    }

    public void RemoveUIText(LocalizatedText text)
    {
        texts.Remove(text);
    }

    private void LoadData(Language language)
    {
        _content.Clear();

        XmlDocument xmlDocument = new XmlDocument();

        TextAsset textAsset = (TextAsset)Resources.Load("LocalizationTexts");
        path = textAsset.text;
        xmlDocument.LoadXml(path);


        if (xmlDocument == null)
        {
            System.Console.WriteLine("Couldnt Load Xml");
            return;
        }

        XmlNode xNode = xmlDocument.ChildNodes.Item(1).ChildNodes.Item(0);

        foreach (XmlNode node in xNode.ChildNodes)
        {
            if (node.LocalName == "TextKey")
            {
                string value = node.Attributes.GetNamedItem("name").Value;
                string text = string.Empty;
                foreach (XmlNode langNode in node)
                {
                    if (langNode.LocalName == language.ToString())
                    {
                        text = langNode.InnerText;

                        if (_content.ContainsKey(value))
                        {
                            _content.Remove(value);
                            _content.Add(value, value + " has been found multiple times in the XML allowed only once!");
                        }
                        else
                        {
                            _content.Add(value, (!string.IsNullOrEmpty(text)) ? text : ("No Text for " + value + " found"));
                        }
                        break;
                    }
                }
            }
        }
    }
}
