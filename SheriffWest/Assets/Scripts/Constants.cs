using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{

    #region UI

    public const string uiShow = "Show";
    public const string uiHide = "Hide";
    public const string uiPopup = "Popup";

    public const string startingSceneName = "MainMenu";
    public const string persistentSceneName = "PersistentScene";

    #endregion

    #region Anim

    public const string dieTrigger = "die";
    public const string shootTrigger = "shoot";
    public const string damageTrigger = "damage";
    
    #endregion

    #region Objects

    public const string bulletPoolTagP = "BulletP";
    public const string bulletPoolTagE = "BulletE";

    #endregion

    #region Gameplay

    public const string gameReady = "ready";
    public const string gameSteady = "steady";
    public const string gameDraw = "draw!";


    #endregion

    #region Audio

    public const string s_music = "BackgroundMusic";
    public const string s_click = "Click";
    public const string s_impact = "Impact";
    public const string s_groundHit = "GroundHit";
    public const string s_pistolShot = "PistolShot";
    public const string s_chickenCluck = "ChickenCluck";
    public const string s_chickenCluck1 = "ChickenCluck1";

    #endregion

    #region PlayerPrefs

    public const string pp_quality = "quality";
    public const string pp_resolution = "resolution";
    public const string pp_fullscreen = "fullScreen";
    public const string pp_musicVolume = "musicVolume";
    public const string pp_soundVolume = "soundVolume";
    public const string pp_firstTime = "settingsFirstTime";


    #endregion

}
