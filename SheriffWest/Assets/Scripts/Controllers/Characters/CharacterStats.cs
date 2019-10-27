using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [Header("General Settings: ")]
    public int maxLives;

    [HideInInspector]
    public bool isDead;

    #region Private Vars

    private int currentLives;

    #endregion

    public event System.Action<float> OnHealthChanged = delegate { };

    private void Start()
    {

        isDead = false;
        currentLives = maxLives;

    }

    public bool ModifyLives(int amount) {

        currentLives += amount;

        float livesPercentage = currentLives / (float)maxLives;
        OnHealthChanged(livesPercentage);

        isDead = currentLives < 1;

        return isDead;

    }

}
