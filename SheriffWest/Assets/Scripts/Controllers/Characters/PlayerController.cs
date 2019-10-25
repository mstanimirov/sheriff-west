using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable, IShooter
{

    #region Private Vars

    private bool    isCounting;
    private float   reactionTime;

    private CharacterAnim characterAnim;
    private CharacterStats characterStats;

    #endregion

    #region Getters/Setters

    public bool IsDead() => characterStats.isDead;

    public float GetReactionTime() => reactionTime;

    public string GetName() => tag;

    public Vector3 GetPosition() => transform.position;

    #endregion

    private void Start()
    {
        
        characterStats = GetComponent<CharacterStats>();
        characterAnim = GetComponentInChildren<CharacterAnim>();

        characterAnim.OnAnimationOver += GameController.instance.EndRound;

    }

    private void Update()
    {

        if (isCounting)
            reactionTime += Time.deltaTime;

    }

    private void OnDisable()
    {

        characterAnim.OnAnimationOver -= GameController.instance.EndRound;

    }

    public void TakeDamage()
    {

        if (characterStats.ModifyLives(-1))
            characterAnim.Die();
        else
            characterAnim.TakeDamage();

    }

    public void OnAttack(IDamageable target)
    {

        characterAnim.Attack();
        target.TakeDamage();

    }

    public void StartRound()
    {

        reactionTime = 0;
        isCounting = true;

    }

    public void FinishRound()
    {

        isCounting = false;

    }

}
