using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{

    public GameObject groundHitEffect;

    public System.Action OnDeathOver = delegate { };
    public System.Action OnContactOver = delegate { };
    public System.Action OnHitAnimOver = delegate { };
    public System.Action OnAttackAnimOver = delegate { };

    public System.Action OnShoot = delegate { };
    public System.Action OnLeftHandShoot = delegate { };
    public System.Action OnRightHandShoot = delegate { };

    #region Private Vars

    private Animator characterAnimator;
    private AudioManager audioManager;

    #endregion

    private void Awake()
    {

        audioManager = AudioManager.instance;
        characterAnimator = GetComponent<Animator>();

    }

    public void Die()
    {

        characterAnimator.SetTrigger(Constants.dieTrigger);

    }


    public void Attack()
    {

        characterAnimator.SetTrigger(Constants.shootTrigger);

    }


    public void TakeDamage()
    {

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

    public void OnTakeDamage()
    {

        OnHitAnimOver();

    }

    public void ShakeCamera()
    {

        if (groundHitEffect)
            groundHitEffect.SetActive(true);

        if (audioManager)
            audioManager.PlaySound(Constants.s_groundHit);

        CameraController.instance.Shake(.15f, .05f);

    }

    public void Shoot() {

        OnShoot();

    }

    public void LeftHandShoot() {

        OnLeftHandShoot();

    }

    public void RightHandShoot() {

        OnRightHandShoot();

    }

    #endregion

}