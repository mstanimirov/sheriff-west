using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpio : EnemyController
{

    [Header("Custom Settings: ")]
    public int speed;
    public GameObject bloodParticle;

    #region Private Vars

    private int direction;
    private float reachedDistance = 0.1f;

    private State state;
    private IDamageable target;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Vector3 smoothedPosition;

    private event System.Action OnSlideComplete;

    #endregion

    public enum State
    {

        Idle,
        Sliding

    }

    protected override void Start()
    {

        base.Start();

        characterAnim.OnContactOver += DealDamage;
        characterAnim.OnAttackAnimOver += SlideBack;

    }

    private void Update()
    {

        switch (state)
        {

            case State.Idle:
                break;
            case State.Sliding:

                smoothedPosition = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
                transform.position = smoothedPosition;

                if (Vector3.Distance(transform.position, targetPosition) < reachedDistance)
                {

                    transform.position = targetPosition;
                    OnSlideComplete?.Invoke();

                }

                break;

        }

    }

    public override void OnAttack(IDamageable target)
    {

        this.target = target;
        initialPosition = transform.position;
        Vector3 slideTo = target.GetPosition() + (GetPosition() - target.GetPosition()).normalized * 2f;

        SlideToTarget(slideTo, () =>
        {

            state = State.Idle;
            base.OnAttack(target);

        });

    }

    public void DealDamage()
    {

        target.TakeDamage(-1);
        CameraController.instance.Shake(.15f, .05f);

    }

    public void SlideToTarget(Vector3 targetPosition, System.Action OnSlideComplete)
    {

        this.targetPosition = targetPosition;
        this.OnSlideComplete = OnSlideComplete;

        state = State.Sliding;

    }

    public void SlideBack()
    {

        SlideToTarget(initialPosition, () =>
        {

            state = State.Idle;

        });

    }

    public override void TakeDamage(int amount)
    {

        bloodParticle.SetActive(true);
        base.TakeDamage(amount);

    }

}
