using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyController
{

    [Header("Custom Settings: ")]
    public GameObject bloodParticle;

    #region Private Vars

    private Weapon gun;

    #endregion

    protected override void Awake()
    {

        base.Awake();
        gun = GetComponentInChildren<Weapon>();

    }

    protected override void Start()
    {

        base.Start();
        gun.SetAudioName(Constants.s_pistolShot + enemyName);

    }

    public override void OnAttack(IDamageable target)
    {

        base.OnAttack(target);
        gun.Shoot();

    }

    public override void TakeDamage(int amount)
    {

        bloodParticle.SetActive(true);
        base.TakeDamage(amount);

    }

    public override void OnDeath()
    {

        base.OnDeath();
        GameController.instance.UnlockNextLevel();

    }

}
