using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    [Header("General Settings: ")]
    public Transform    buttonParent;
    public LevelButton  buttonPrefab;

    #region Private Vars

    private int levelAt;
    private int totalLevels;

    private List<LevelButton> buttons = new List<LevelButton>();

    #endregion

    private void Start()
    {

        LoadLevelData();
        SetLevelButton();

    }

    public void LoadLevelData()
    {

        levelAt = GameManager.instance.levelAt;
        totalLevels = SceneManager.sceneCountInBuildSettings - 4;

        for (int i = 0; i < totalLevels; i++)
        {

            LevelButton currentButton = Instantiate(buttonPrefab, buttonParent);
            buttons.Add(currentButton);
            
        }

    }

    public void SetLevelButton() {

        for (int i = 0; i < totalLevels; i++)
        {

            buttons[i].SetButton(i + 1, i <= levelAt);

        }

    }

    public void MenuClick()
    {

        GameManager.instance.MainMenu();

    }

}
