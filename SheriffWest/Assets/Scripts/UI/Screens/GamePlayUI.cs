using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{

    [Header("Object References:")]
    public Animator btnAnimator;
    public Animator btnAnimator2;
    public TextMeshProUGUI  infoText;
    public TextMeshProUGUI  timerText;

    #region Private Vars

    private Animator infoAnimator;
    private Animator timerAnimator;

    #endregion

    private void Awake()
    {

        infoAnimator = infoText.GetComponentInParent<Animator>();
        timerAnimator = timerText.GetComponent<Animator>();

    }

    public void ShowInfo(string value)
    {

        infoText.SetText(value);
        infoAnimator.SetTrigger(Constants.uiShow);

    }

    public void SetTimerText(string value)
    {

        timerText.SetText(value);
        timerAnimator.SetTrigger(Constants.uiPopup);

    }

    public void UnlockNextLevel() {

        btnAnimator.SetTrigger("Show");

    }

    public void RestartLevel()
    {

        btnAnimator2.SetTrigger("Show");

    }

}
