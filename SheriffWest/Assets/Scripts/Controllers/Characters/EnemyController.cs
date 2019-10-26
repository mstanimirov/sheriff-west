using System;
using System.Collections;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour, IDamageable, IShooter
{

    public bool     isEnemy;
    public string   enemyName;

    public float minReactionTime;
    public float maxReactionTime;

    #region Private Vars

    private bool isCrRunning;
    private float reactionTime;

    private CharacterAnim characterAnim;
    private CharacterStats characterStats;

    #endregion

    #region Getters/Setters

    public bool IsDead() => characterStats.isDead;

    public float GetReactionTime() => reactionTime;

    public string GetName() => enemyName;

    public Vector3 GetPosition() => transform.position;

    #endregion

    protected virtual void Start()
    {

        characterStats = GetComponent<CharacterStats>();
        characterAnim = GetComponentInChildren<CharacterAnim>();

        characterAnim.OnAnimationOver += GameController.instance.EndRound;

    }

    private void Update()
    {



    }

    private void OnDisable()
    {

        characterAnim.OnAnimationOver -= GameController.instance.EndRound;

    }

    public virtual void TakeDamage()
    {

        if (characterStats.ModifyLives(-1))
            characterAnim.Die();
        else
            characterAnim.TakeDamage();

    }

    public virtual void OnAttack(IDamageable target)
    {

        characterAnim.Attack();

    }

    #region Round

    public virtual void StartRound()
    {

        reactionTime = UnityEngine.Random.Range(minReactionTime, maxReactionTime);
        StartCoroutine(AttackCountDown());

    }

    public virtual void FinishRound()
    {

        if (isCrRunning)
            StopAllCoroutines();

    }

    private IEnumerator AttackCountDown()
    {

        isCrRunning = true;

        yield return new WaitForSeconds(reactionTime);

        isCrRunning = false;
        GameController.instance.FirstToAttackE();

    }

    #endregion

}
