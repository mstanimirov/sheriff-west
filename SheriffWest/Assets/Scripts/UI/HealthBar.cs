using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [Header("General Settings:")]
    public float updateTime;

    [Header("Object Reference:")]
    public GameObject healthBar;
    public Image foreground;
    public Image foreground2;

    #region Private Vars

    private CharacterStats characterStats;

    #endregion

    private void Start()
    {

        characterStats = GetComponentInParent<CharacterStats>();
        characterStats.OnHealthChanged += HandleHealthChanged;

        healthBar.SetActive(false);

    }

    private void HandleHealthChanged(float percent)
    {


        if (percent != 0)
            healthBar.SetActive(true);
        foreground2.fillAmount = percent;

        StopAllCoroutines();
        StartCoroutine(ChangeToPercent(percent));

    }

    private IEnumerator ChangeToPercent(float percent)
    {

        float preChangePercent = foreground.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < updateTime * 2)
        {

            elapsedTime += Time.deltaTime;
            foreground.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsedTime / updateTime);

            yield return null;

        }

        elapsedTime = 0f;
        while (elapsedTime < updateTime)
        {

            elapsedTime += Time.deltaTime;
            yield return null;

        }

        foreground.fillAmount = percent;
        healthBar.SetActive(false);

    }

}
