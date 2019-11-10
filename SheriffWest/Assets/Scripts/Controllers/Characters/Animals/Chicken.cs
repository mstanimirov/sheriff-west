using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : EnemyController
{

    [Header("Custom Settings: ")]
    public int speed;
    public bool isLeft;

    #region Private Vars

    private int     direction;
    private float   reachedDistance = 0.1f;

    private State   mobState;
    private Vector3 targetPosition;
    private Vector3 smoothedPosition;

    private event System.Action OnMoveComplete;

    #endregion

    private enum State
    {

        Idle,
        Running

    }

    protected override void Awake()
    {

        base.Awake();        

    }

    protected override void Start()
    {

        base.Start();

        direction = isLeft ? -2 : 2;
        characterAnim.OnAttackAnimOver += GameController.instance.EndRound;

    }

    protected override void OnDisable()
    {

        base.OnDisable();
        characterAnim.OnAttackAnimOver -= GameController.instance.EndRound;

    }

    private void Update()
    {

        switch (mobState)
        {

            case State.Idle:
                break;
            case State.Running:

                smoothedPosition = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
                transform.position = smoothedPosition;

                if (Vector3.Distance(transform.position, targetPosition) < reachedDistance)
                {

                    transform.position = targetPosition;
                    OnMoveComplete?.Invoke();

                }

                break;

        }

    }

    public override void Target(bool target)
    {

        base.Target(target);
        gameObject.layer = target ? 11 : 10;

    }

    public override void OnAttack(IDamageable target)
    {

        base.OnAttack(target);

        if (audioManager)
            audioManager.PlaySound(Constants.s_chickenCluck);

        MoveAway(() => {

            direction *= -1;
            mobState = State.Idle;

        });

    }

    public override void TakeDamage(int amount)
    {

        base.TakeDamage(amount);

        if (audioManager)
            audioManager.PlaySound(Constants.s_chickenCluck1);

    }

    private void MoveAway(System.Action OnMoveComplete)
    {

        targetPosition = transform.position + (Vector3.right * direction);
        this.OnMoveComplete = OnMoveComplete;

        mobState = State.Running;

    }

}
