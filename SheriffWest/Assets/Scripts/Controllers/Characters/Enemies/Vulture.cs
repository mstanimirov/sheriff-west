using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulture : EnemyController
{

    [Header("Custom Settings: ")]
    public GameObject bloodParticle;
    public EnemyController minion;

    #region Private Vars

    private Weapon[] guns;
    private IDamageable target;

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

        characterAnim.OnAttackAnimOver += OnAttackOver;

        guns[0].SetAudioName(Constants.s_pistolShot + enemyName);
        guns[1].SetAudioName(Constants.s_pistolShot + enemyName);

    }

    protected override void OnDisable()
    {

        base.OnDisable();

        characterAnim.OnLeftHandShoot -= guns[0].Shoot;
        characterAnim.OnRightHandShoot -= guns[1].Shoot;

        characterAnim.OnAttackAnimOver -= OnAttackOver;

    }

    public override void OnAttack(IDamageable target)
    {

        base.OnAttack(target);
        this.target = target;

    }

    public override void TakeDamage(int amount)
    {

        base.TakeDamage(amount);
        bloodParticle.SetActive(true);

        if (characterStats.currentLives == 1) {

            SpawnMinion();

        }

    }

    public override void OnDeath()
    {

        base.OnDeath();
        GameController.instance.UnlockNextLevel();

    }

    public void SpawnMinion() {

        if (minion.gameObject.activeSelf == true)
            return;

        minion.gameObject.SetActive(true);
        GameController.instance.enemies.Add(minion);

    }

    public void OnAttackOver() {

        if (minion.gameObject.activeSelf == false)
            return;

        if (minion.IsDead())
            return;

        if (target != null)
            minion.OnAttack(target);

    }

}
