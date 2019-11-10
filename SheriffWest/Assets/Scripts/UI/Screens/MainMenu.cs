using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void PlayClick()
    {

        GameManager.instance.StartGame();
        AudioManager.instance.PlaySound(Constants.s_click);

    }

    public void SettingsClick() {

        GameManager.instance.Settings();
        AudioManager.instance.PlaySound(Constants.s_click);

    }

    public void QuitClick() {

        Application.Quit();
        AudioManager.instance.PlaySound(Constants.s_click);

    }

}
