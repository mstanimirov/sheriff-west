﻿using System;
using System.Collections;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour, IDamageable, IShooter
{

    [Header("General Settings:")]
    public bool     isEnemy;
    public string   enemyName;
    public string   enemyDeadName;

    [Header("Combat Settings: ")]
    public float minReactionTime;
    public float maxReactionTime;

    [Header("Object Reference: ")]
    public GameObject liveGraphic;
    public GameObject deadGraphic;

    [HideInInspector]
    public bool isTarget;

    #region Private Vars

    private bool isCrRunning;
    private float reactionTime;

    protected CharacterAnim characterAnim;
    protected CharacterStats characterStats;

    #endregion

    #region Getters/Setters

    public bool IsDead() => characterStats.isDead;

    public float GetReactionTime() => reactionTime;

    public string GetName() => IsDead() ? enemyDeadName : enemyName;

    public Vector3 GetPosition() => transform.position;

    #endregion

    protected virtual void Start()
    {

        isTarget = false;

        characterStats = GetComponent<CharacterStats>();
        characterAnim = GetComponentInChildren<CharacterAnim>();

        characterAnim.OnDeathOver += OnDeath;
        characterAnim.OnHitAnimOver += GameController.instance.EndRound;

    }

    private void OnDisable()
    {

        characterAnim.OnDeathOver -= OnDeath;
        characterAnim.OnHitAnimOver -= GameController.instance.EndRound;

    }

    public virtual void Target() {

        isTarget = true;

    }

    public virtual void RemoveTarget() {

        isTarget = false;

    }

    public virtual void TakeDamage(int amount)
    {

        if (characterStats.ModifyLives(amount))
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
    public void OnDeath() {

        GameController.instance.EndRound();

        if (deadGraphic) {

            liveGraphic.SetActive(false);
            deadGraphic.SetActive(true);

        }

    }


}
