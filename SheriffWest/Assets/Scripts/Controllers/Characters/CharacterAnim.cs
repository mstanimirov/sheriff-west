﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{

    public System.Action OnDeathOver = delegate { };
    public System.Action OnContactOver = delegate { };
    public System.Action OnHitAnimOver = delegate { };
    public System.Action OnAttackAnimOver = delegate { };

    #region Private Vars

    private Animator characterAnimator;

    #endregion

    private void Start()
    {

        characterAnimator = GetComponent<Animator>();

    }

    public void Die() {

        characterAnimator.SetTrigger(Constants.dieTrigger);

    }
    

    public void Attack() {

        characterAnimator.SetTrigger(Constants.shootTrigger);

    }


    public void TakeDamage() {

        characterAnimator.SetTrigger(Constants.damageTrigger);

    }

    #region Event Functions

    public void OnDie()
    {

        OnDeathOver();

    }

    public void OnAttack()
    {

        OnAttackAnimOver();

    }

    public void OnAttackContact()
    {

        OnContactOver();

    }

    public void OnTakeDamage() {

        OnHitAnimOver();

    }

    public void ShakeCamera() {

        CameraController.instance.Shake(.15f, .05f);

    }

    #endregion

}
