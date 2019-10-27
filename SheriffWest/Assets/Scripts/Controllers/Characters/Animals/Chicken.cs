﻿using System.Collections;
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

    protected override void Start()
    {

        base.Start();

        direction = isLeft ? -2 : 2;
        characterAnim.OnAttackAnimOver += GameController.instance.EndRound;

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

    public override void OnAttack(IDamageable target)
    {

        base.OnAttack(target);
        MoveAway(() => {

            direction *= -1;
            mobState = State.Idle;

        });

    }

    private void MoveAway(System.Action OnMoveComplete)
    {

        targetPosition = transform.position + (Vector3.right * direction);
        this.OnMoveComplete = OnMoveComplete;

        mobState = State.Running;

    }

}
