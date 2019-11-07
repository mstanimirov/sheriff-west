using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{

    //[HideInInspector]
    public int levelToLoad;

    #region Private Vars

    private Button button;
    private TextMeshProUGUI levelText;

    #endregion

    private void Awake()
    {

        button = GetComponentInChildren<Button>();
        levelText = GetComponentInChildren<TextMeshProUGUI>();

    }

    public void LoadLevel() {

        GameManager.instance.LoadLevel(levelToLoad);

    }

    public void SetButton(int level, bool active) {

        levelToLoad = level - 1;
        levelText.SetText(level.ToString());

        button.interactable = active;

    }

}
