using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ProgressControllerScript : MonoBehaviour
{
    List<string> themes;
    List<string> themesRu;
    [SerializeField] TMP_Text output;
    StringBuilder text;
    float[] records;

    void Start()
    {
        themes = new List<string> {"Culture", "Ontology", "Anthropology", "Gnoseology", "Science", "Law"};
        themesRu = new List<string> { "Философия культуры", "Онтология", "Антропология", "Гносеология", "Наука и техника", "Философия права"};
        records = new float[themes.Count];
        LoadRecords();
    }

    public void LoadRecords()
    {
        text = new StringBuilder();
        foreach (var theme in themes)
        {
            var index = themes.IndexOf(theme);
            if (PlayerPrefs.HasKey(theme))
                records[index] = PlayerPrefs.GetFloat(theme);
            else
                PlayerPrefs.SetFloat(theme, 0);
            text.AppendLine(string.Format("{0} : {1}%", themesRu[index], records[index]));
            output.text = text.ToString();
        }
    }

    public void DeleteRecords()
    {
        PlayerPrefs.DeleteAll();
    }
}