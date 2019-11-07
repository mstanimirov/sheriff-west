using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : EnemyController
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

        characterAnim.OnShoot += gun.Shoot;

    }

    protected override void OnDisable()
    {

        base.OnDisable();
        characterAnim.OnShoot -= gun.Shoot;

    }

    public override void OnAttack(IDamageable target)
    {

        base.OnAttack(target);

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
