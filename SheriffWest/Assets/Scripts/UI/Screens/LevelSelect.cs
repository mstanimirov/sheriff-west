using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    [Header("General Settings: ")]
    public Button[] buttons;

    private void Start()
    {

        LoadLevelData();

    }

    public void MenuClick()
    {

        GameManager.instance.MainMenu();

    }

    public void LoadLevelData() {

        int levelAt = GameManager.instance.levelAt;

        for (int i = 0; i < buttons.Length; i++)
        {

            if (i > levelAt)
                buttons[i].interactable = false;

        }

    }

}
