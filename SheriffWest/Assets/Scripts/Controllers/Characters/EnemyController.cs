using System;
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
    public Sprite thumb;

    public GameObject infoPanel;
    public GameObject liveGraphic;
    public GameObject deadGraphic;

    [HideInInspector]
    public bool isTarget;
    
    #region Private Vars

    private bool isCrRunning;
    private float reactionTime;

    protected AudioManager audioManager;
    protected CharacterAnim characterAnim;
    protected CharacterStats characterStats;

    #endregion

    #region Getters/Setters

    public bool IsDead() => characterStats.isDead;

    public float GetReactionTime() => reactionTime;

    public string GetCharacterName() => IsDead() ? enemyDeadName : enemyName;

    public Sprite GetThumb() => thumb;

    public Vector3 GetPosition() => transform.position;

    public CharacterStats GetCharacterStats() => characterStats;

    #endregion

    protected virtual void Awake()
    {
        
        characterStats = GetComponent<CharacterStats>();
        characterAnim = GetComponentInChildren<CharacterAnim>();        

    }

    protected virtual void Start()
    {

        isTarget = false;

        audioManager = AudioManager.instance;
        characterAnim.OnDeathOver += OnDeath;
        characterAnim.OnHitAnimOver += GameController.instance.EndRound;

    }

    protected virtual void OnDisable()
    {

        characterAnim.OnDeathOver -= OnDeath;
        characterAnim.OnHitAnimOver -= GameController.instance.EndRound;

    }

    public virtual void Target(bool target) {

        isTarget = target;
        infoPanel.SetActive(target);

    }

    public virtual void TakeDamage(int amount)
    {

        if (characterStats.ModifyLives(amount))
            characterAnim.Die();
        else
            characterAnim.TakeDamage();

        if (audioManager)
            audioManager.PlaySound(Constants.s_impact);

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
    public virtual void OnDeath() {

        GameController.instance.EndRound();

        if (deadGraphic) {

            liveGraphic.SetActive(false);
            deadGraphic.SetActive(true);

        }

    }


}
