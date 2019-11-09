using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [Header("Objects: ")]
    public Toggle fullScreenToggle;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;

    #region Private Vars

    private int musicVolume;
    private int soundVolume;
    private int qualityIndex;
    private int resolutionIndex;

    private bool isFullscreen;

    private Resolution[] resolutions;

    #endregion

    private void Start()
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for(int i = 0; i < resolutions.Length; i++) {

            string option = resolutions[i].width + " x" + resolutions[i].height;
            options.Add(option);

        }

        resolutionDropdown.AddOptions(options);
        LoadSettingsData();

    }

    public void MenuClick()
    {

        GameManager.instance.MainMenu();
        SaveSettingsData();

    }

    public void SetQuality(int qualityIndex) {

        this.qualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);

    }

    public void SetFullscreen(bool isFullscreen) {

        this.isFullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;

    }

    public void SetResolution(int resolutionIndex) {

        this.resolutionIndex = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    public void SaveSettingsData() {

        PlayerPrefs.SetInt("quality", qualityDropdown.value);
        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("fullscreen", fullScreenToggle.isOn ? 1 : 0);

    }

    public void LoadSettingsData() {

        if (PlayerPrefs.GetInt("settingsFirstTime") != 0)
        {

            qualityIndex = PlayerPrefs.GetInt("quality");
            resolutionIndex = PlayerPrefs.GetInt("resolution");
            isFullscreen = PlayerPrefs.GetInt("fullscreen") == 1;

            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutionIndex;
            resolutionDropdown.RefreshShownValue();

            Screen.fullScreen = isFullscreen;
            fullScreenToggle.isOn = isFullscreen;

            QualitySettings.SetQualityLevel(qualityIndex);
            qualityDropdown.value = qualityIndex;
            qualityDropdown.RefreshShownValue();

        }
        else {

            PlayerPrefs.SetInt("settingsFirstTime", 1);

        }

    }

}
