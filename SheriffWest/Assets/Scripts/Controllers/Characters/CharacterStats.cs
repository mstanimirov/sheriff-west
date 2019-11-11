using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [Header("General Settings: ")]
    public int maxLives;

    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public int currentLives;

    #region Private Vars

    private BoxCollider boxCollider;

    #endregion

    public event System.Action<float> OnHealthChanged = delegate { };

    private void Awake()
    {

        boxCollider = GetComponent<BoxCollider>();

    }

    private void Start()
    {

        isDead = false;
        currentLives = maxLives;

    }

    public bool ModifyLives(int amount) {

        currentLives += amount;

        float livesPercentage = currentLives / (float)maxLives;
        OnHealthChanged(livesPercentage);

        if (currentLives < 1) {

            isDead = true;
            boxCollider.enabled = false;


        }

        return isDead;

    }

}
