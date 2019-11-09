using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    public void PlayClick()
    {

        GameManager.instance.StartGame();

    }

    public void SettingsClick() {

        GameManager.instance.Settings();

    }

    public void QuitClick() {

        Application.Quit();

    }

}
