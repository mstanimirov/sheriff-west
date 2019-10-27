using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{

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

        characterAnimator.SetTrigger("die");

    }
    

    public void Attack() {

        characterAnimator.SetTrigger("shoot");

    }


    public void TakeDamage() {

        characterAnimator.SetTrigger("damage");

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

    public void OnTakeDamage() {

        OnHitAnimOver();

    }

    #endregion

}
