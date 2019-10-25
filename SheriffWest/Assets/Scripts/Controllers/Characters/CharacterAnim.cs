using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{

    public System.Action OnAnimationOver = delegate { };

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

        OnAnimationOver();

    }

    public void OnAttack()
    {

        //OnAnimationOver();

    }

    public void OnTakeDamage() {

        OnAnimationOver();

    }

    #endregion

}
