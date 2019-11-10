using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [HideInInspector]
    public int levelAt;

    [Header("General Settings: ")]    
    public int maxLevels;

    [Header("References: ")]
    public SceneController sceneController;

    [Header("Debug Settings: ")]
    public bool resetOnStart;

    #region Private Vars



    #endregion

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

    }

    private void Start()
    {

        if (resetOnStart)
            ResetData();

        LoadData();
        maxLevels = SceneManager.sceneCountInBuildSettings - 4;
        AudioManager.instance.PlaySound(Constants.s_music);

    }

    #region Player Save

    public void SaveData()
    {

        SaveSystem.SaveData(this);

    }

    public void LoadData()
    {

        PlayerData data = SaveSystem.LoadData();

        if (data != null)
            levelAt = data.levelAt;
        else
            ResetData();

    }

    public void ResetData()
    {

        levelAt = 0;
        SaveSystem.SaveData(this);
        PlayerPrefs.DeleteAll();

    }

    #endregion

    #region Button Functions

    public void MainMenu()
    {

        sceneController.FadeAndLoadScene("MainMenu");

    }

    public void StartGame()
    {

        sceneController.FadeAndLoadScene("LevelSelect");
        //AudioManager.instance.StopSound(Constants.s_music);

    }

    public void Settings()
    {

        sceneController.FadeAndLoadScene("Settings");

    }

    public void LoadLevel(int levelToLoad)
    {

        if (levelToLoad != maxLevels)
            sceneController.FadeAndLoadScene("Level" + levelToLoad);
        else
            MainMenu();

    }

    #endregion

}
