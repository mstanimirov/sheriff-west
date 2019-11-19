using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{

    [Header("General Settings: ")]
    public GameObject shootTutorial;
    public GameObject targetTutorial;

    public void ShowShootTutorial() {

        if (targetTutorial.activeSelf)
            targetTutorial.SetActive(false);

        shootTutorial.SetActive(true);

    }

    public void ShowTargetTutorial()
    {

        if (shootTutorial.activeSelf)
            shootTutorial.SetActive(false);

        targetTutorial.SetActive(true);

    }

    public void HideAll() {

        shootTutorial.SetActive(false);
        targetTutorial.SetActive(false);

        gameObject.SetActive(false);

    }

}
