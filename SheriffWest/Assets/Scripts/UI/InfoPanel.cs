using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{

    [Header("General Settings:")]
    public bool  isEnemy;
    public float updateTime;

    public Color[] redBar;
    public Color[] greenBar; 

    [Header("Object References: ")]
    public Image thumb;
    public Image foreground;
    public Image foreground2;

    public TextMeshProUGUI targetName;

    #region Private Vars

    private IShooter target;
    private CharacterStats targetStats;

    #endregion

    private void Awake()
    {

        target = GetComponentInParent<IShooter>();

    }

    private void Start()
    {

        targetStats = target.GetCharacterStats();
        targetStats.OnHealthChanged += HandleHealthChanged;

        thumb.sprite = target.GetThumb();
        targetName.SetText(target.GetCharacterName());

        if (isEnemy)
        {

            foreground.color = redBar[0];
            foreground2.color = redBar[1];

        }
        else {

            foreground.color = greenBar[0];
            foreground2.color = greenBar[1];

        }

        foreground.fillAmount = 1;
        foreground2.fillAmount = 1;

    }

    private void OnDisable()
    {
        
        targetStats.OnHealthChanged -= HandleHealthChanged;

    }

    private void HandleHealthChanged(float percent)
    {

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

        foreground.fillAmount = percent;
        targetName.SetText(target.GetCharacterName());

    }


}
