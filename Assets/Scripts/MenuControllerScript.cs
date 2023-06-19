using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerScript : MonoBehaviour
{
    public Canvas[] Canvases;
    List<string> CanvasesNames;
    // Start is called before the first frame update
    void Start()
    {
        CanvasesNames = new List<string> { "MenuCanvas", "SettingsCanvas", "ThemesCanvas" };
        Canvases = FindObjectsOfType<Canvas>(true);
    }

    public void SwitchCanvases(string desiredCanvas)
    {
        foreach(var canvas in Canvases)
        {
            if (canvas.name == desiredCanvas)
                canvas.gameObject.SetActive(true);
            else
                canvas.gameObject.SetActive(false);
        }
    }
}