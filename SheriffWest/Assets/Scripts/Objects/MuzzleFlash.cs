using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{

    [Header("Objects: ")]
    public GameObject flash;
    public GameObject burst;
    public GameObject smoke;
    public GameObject pointLight;

    public void Show() {

        StartCoroutine(Timer());

    }

    public IEnumerator Timer() {


        flash.SetActive(false);
        burst.SetActive(false);
        smoke.SetActive(false);

        flash.SetActive(true);
        burst.SetActive(true);
        smoke.SetActive(true);
        pointLight.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        pointLight.SetActive(false);

        yield return null;

    }

}
