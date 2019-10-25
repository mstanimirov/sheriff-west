using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{

    public GameObject       panel;    

    public TextMeshProUGUI  infoText;
    public TextMeshProUGUI  timerText;
    public TextMeshProUGUI  shooterText;

    #region Private Vars

    private Animator infoAnimator;
    private Animator timerAnimator;
    private Animator panelAnimator;

    #endregion

    private void Awake()
    {

        infoAnimator = infoText.GetComponentInParent<Animator>();
        panelAnimator = panel.GetComponent<Animator>();
        timerAnimator = timerText.GetComponent<Animator>();

    }    

    public void ShowInfo(string value)
    {

        infoText.SetText(value);
        infoAnimator.SetTrigger("Show");

    }

    public void ShowPanel(bool show)
    {

        panelAnimator.SetTrigger(show ? "Show" : "Hide");

    }

    public void SetTimerText(string value)
    {

        timerText.SetText(value);
        timerAnimator.SetTrigger("Popup");

    }

    public void SetShooterText(string value)
    {

        shooterText.SetText(value);

    }

}
