using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable, IShooter
{

    [Header("General Settings: ")]
    public int speed;
    public string playerName;

    [Header("Object References: ")]
    public Sprite thumb;

    public GameObject liveGraphic;
    public GameObject deadGraphic;

    public GameObject bloodParticle;

    #region Private Vars

    private int direction;

    private State playerState;

    private float reactionTime;
    private float reachedDistance = 0.1f;

    private Vector3 targetPosition;
    private Vector3 smoothedPosition;

    private Weapon gun;

    private AudioManager audioManager;
    private CharacterAnim characterAnim;
    private CharacterStats characterStats;

    private event Action OnAlignComplete;

    #endregion

    #region IShoote/Idamageable

    public bool IsDead() => characterStats.isDead;

    public float GetReactionTime() => reactionTime;

    public string GetCharacterName() => playerName;

    public Sprite GetThumb() => thumb;

    public Vector3 GetPosition() => transform.position;

    public CharacterStats GetCharacterStats() => characterStats;

    #endregion

    public enum State
    {

        Idle,
        Moving,
        Waiting

    }

    private void Awake()
    {

        gun = GetComponentInChildren<Weapon>();

        characterStats = GetComponent<CharacterStats>();
        characterAnim = GetComponentInChildren<CharacterAnim>();        

    }

    private void Start()
    {

        audioManager = AudioManager.instance;
        characterAnim.OnDeathOver += OnDeath;
        characterAnim.OnHitAnimOver += GameController.instance.EndRound;

    }

    private void Update()
    {

        switch (playerState)
        {

            case State.Idle:
                break;

            case State.Moving:

                smoothedPosition = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
                transform.position = smoothedPosition;

                if (Vector3.Distance(transform.position, targetPosition) < reachedDistance)
                {

                    transform.position = targetPosition;
                    OnAlignComplete?.Invoke();

                }

                break;

            case State.Waiting:

                reactionTime += Time.deltaTime;

                break;

        }

    }

    private void OnDisable()
    {

        characterAnim.OnDeathOver -= OnDeath;
        characterAnim.OnHitAnimOver -= GameController.instance.EndRound;

    }

    #region Shooter/Damage Methods

    public void TakeDamage(int amount)
    {

        bloodParticle.SetActive(true);

        if (audioManager)
            audioManager.PlaySound(Constants.s_impact);

        if (characterStats.ModifyLives(amount))
            characterAnim.Die();
        else
            characterAnim.TakeDamage();

    }

    public void OnAttack(IDamageable target)
    {

        characterAnim.Attack();
        gun.Shoot();

    }

    #endregion

    public void StartRound()
    {

        reactionTime = 0;
        playerState = State.Waiting;

    }

    public void FinishRound()
    {

        playerState = State.Idle;

    }

    public void AlignWithTarget(IShooter target, Action OnAlignComplete)
    {

        Vector3 alignTo = new Vector3(target.GetPosition().x, GetPosition().y, GetPosition().z);

        StartAligning(alignTo, () => {

            OnAlignComplete();
            playerState = State.Idle;

        });

    }

    public void StartAligning(Vector3 targetPosition, Action OnAlignComplete)
    {

        this.targetPosition = targetPosition;
        this.OnAlignComplete = OnAlignComplete;

        playerState = State.Moving;

    }

    public void OnDeath() {

        GameController.instance.EndRound();

        liveGraphic.SetActive(false);
        deadGraphic.SetActive(true);

        GameController.instance.ShowReloadButton();

    }

}
