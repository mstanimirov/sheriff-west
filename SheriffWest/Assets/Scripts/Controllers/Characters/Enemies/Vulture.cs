using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulture : EnemyController
{

    [Header("Custom Settings: ")]
    public GameObject bloodParticle;

    #region Private Vars

    private Weapon[] guns;

    #endregion

    protected override void Awake()
    {

        base.Awake();
        guns = GetComponentsInChildren<Weapon>();

    }

    protected override void Start()
    {

        base.Start();

        characterAnim.OnLeftHandShoot += guns[0].Shoot;
        characterAnim.OnRightHandShoot += guns[1].Shoot;

    }

    protected override void OnDisable()
    {

        base.OnDisable();

        characterAnim.OnLeftHandShoot -= guns[0].Shoot;
        characterAnim.OnRightHandShoot -= guns[1].Shoot;

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
