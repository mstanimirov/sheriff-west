using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{

    [Header("Objects: ")]
    public Toggle fullScreenToggle;

    public Slider musicSlider;
    public Slider soundSlider;

    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;

    public AudioMixer audioMixer;

    #region Private Vars

    private int musicVolume;
    private int soundVolume;
    private int qualityIndex;
    private int resolutionIndex;
    private int defaultResolutionIndex;

    private bool isFullscreen;

    private Resolution[] resolutions;

    #endregion

    private void Start()
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        defaultResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {

            string option = resolutions[i].width + " x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                defaultResolutionIndex = i;

        }

        resolutionDropdown.AddOptions(options);
        LoadSettingsData();

    }

    public void MenuClick()
    {

        GameManager.instance.MainMenu();
        AudioManager.instance.PlaySound(Constants.s_click);

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

    public void SetMusicVolume(float volume) {

        audioMixer.SetFloat("MusicVolume", volume * 5);

    }
    
    public void SetSoundVolume(float volume) {

        audioMixer.SetFloat("SoundVolume", volume * 5);
        //AudioManager.instance.PlaySound(Constants.s_chickenCluck);
        AudioManager.instance.PlaySound(Constants.s_click);

    }

    public void SaveSettingsData() {

        PlayerPrefs.SetInt(Constants.pp_quality, qualityDropdown.value);
        PlayerPrefs.SetInt(Constants.pp_resolution, resolutionDropdown.value);
        PlayerPrefs.SetInt(Constants.pp_fullscreen, fullScreenToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat(Constants.pp_musicVolume, musicSlider.value);
        PlayerPrefs.SetFloat(Constants.pp_soundVolume, soundSlider.value);

    }

    public void LoadSettingsData() {

        if (PlayerPrefs.GetInt(Constants.pp_firstTime) != 0)
        {

            qualityIndex = PlayerPrefs.GetInt(Constants.pp_quality);
            resolutionIndex = PlayerPrefs.GetInt(Constants.pp_resolution);
            isFullscreen = PlayerPrefs.GetInt(Constants.pp_fullscreen) == 1;

            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutionIndex;
            resolutionDropdown.RefreshShownValue();

            Screen.fullScreen = isFullscreen;
            fullScreenToggle.isOn = isFullscreen;

            QualitySettings.SetQualityLevel(qualityIndex);
            qualityDropdown.value = qualityIndex;
            qualityDropdown.RefreshShownValue();

            musicSlider.value = PlayerPrefs.GetFloat(Constants.pp_musicVolume);
            soundSlider.value = PlayerPrefs.GetFloat(Constants.pp_soundVolume);

        }
        else {

            PlayerPrefs.SetInt(Constants.pp_firstTime, 1);

            resolutionDropdown.value = defaultResolutionIndex;
            resolutionDropdown.RefreshShownValue();

        }

    }

}
