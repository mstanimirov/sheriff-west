using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable, IShooter
{

    public string enemyName;

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

    private void Start()
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
