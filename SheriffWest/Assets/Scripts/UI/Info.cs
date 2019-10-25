using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{

    public GameObject info;

    #region Private Vars

    private Animator infoAnimator;
    private TextMeshProUGUI infoText;

    #endregion

    private void Start()
    {

        infoAnimator = info.GetComponent<Animator>();
        infoText = info.GetComponentInChildren<TextMeshProUGUI>();

    }

    public void ShowInfoText(string value) {

        infoText.SetText(value);
        infoAnimator.SetTrigger("Show");

    }

}
