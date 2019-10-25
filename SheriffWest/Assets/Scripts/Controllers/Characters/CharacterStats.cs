using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int maxLives;

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
