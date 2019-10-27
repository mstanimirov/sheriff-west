using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{

    public System.Action OnContact = delegate { };
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

        OnHitAnimOver();

    }

    public void OnAttack()
    {

        OnAttackAnimOver();

    }

    public void OnAttackContact()
    {

        OnContact();

    }

    public void OnTakeDamage() {

        OnHitAnimOver();

    }

    #endregion

}
