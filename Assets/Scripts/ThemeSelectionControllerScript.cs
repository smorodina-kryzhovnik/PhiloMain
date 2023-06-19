using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelectionControllerScript : MonoBehaviour
{
    public void GetTextToController(string fileName)
    {
        QuestionControllerScript.theme = fileName;
        QuestionControllerScript.text = Resources.Load<TextAsset>(string.Format("Questions/{0}", fileName));
    }
}