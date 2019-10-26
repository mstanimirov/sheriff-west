using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyController
{

    #region Private Vars

    private Weapon gun;

    #endregion

    protected override void Start()
    {

        base.Start();
        gun = GetComponentInChildren<Weapon>();

    }

    public override void OnAttack(IDamageable target)
    {

        base.OnAttack(target);
        gun.Shoot();

    }

}
