﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    [HideInInspector]
    public static GameController instance;

    [Header("General Settings:")]
    public List<EnemyController> enemies;

    [Header("Debug Settings: ")]
    public bool freeShoot;

    #region Private Vars

    private int target;

    private bool isCrRunning;

    private float delay;

    private GameState gameState;
    private InputMaster inputMaster;

    private GamePlayUI gameplayScreen;
    private AimController aimController;

    private EnemyController enemy;
    private PlayerController player;

    #endregion

    #region Constants

    private readonly WaitForSeconds textRefresh = new WaitForSeconds(1f);

    #endregion

    public enum GameState {

        Target,
        Prep,
        Shot,
        Wait

    }

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;
        inputMaster = new InputMaster();

        player = FindObjectOfType<PlayerController>();

        gameplayScreen = FindObjectOfType<GamePlayUI>();
        aimController = FindObjectOfType<AimController>();

    }

    private void Start()
    {

        delay = 0;
        target = -1;
        gameState = GameState.Target;

    }

    private void Update()
    {

        if (delay > 0)
            delay -= Time.deltaTime;

    }

    private void OnEnable()
    {

        inputMaster.Gameplay.Enable();

        inputMaster.Gameplay.Click.performed += ctx => StartRound();
        inputMaster.Gameplay.RightClick.performed += ctx => SwitchTarget();

    }

    private void OnDisable()
    {

        inputMaster.Gameplay.Click.performed -= ctx => StartRound();
        inputMaster.Gameplay.RightClick.performed -= ctx => SwitchTarget();

        inputMaster.Gameplay.Disable();

    }

    public void StartRound() {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (player.IsDead())
            return;

        if (target < 0)
            return;

        if (delay > 0)
            return;

        if (freeShoot) {

            player.AlignWithTarget(enemy, () => {

                player.OnAttack(enemy);

            });

            return;

        }

        switch (gameState) {

            case GameState.Target:                

                if (!enemy.IsDead()) {

                    player.AlignWithTarget(enemy, () => {

                        StartCountDown();

                    });

                }

                break;

            case GameState.Wait:


                break;

            case GameState.Prep:
            case GameState.Shot:

                FirstToAttackP();

                break;

        }

    }

    public void SwitchTarget() {

        if (player.IsDead())
            return;

        if (enemies.Count < 1)
            return;

        if (gameState != GameState.Target)
            return;

        if (target > -1)
            enemies[target].RemoveTarget();

        target++;
        if (target > enemies.Count - 1)
            target = 0;

        enemy = enemies[target];
        AimAt();

    }

    public void AimAt() {

        aimController.SetTarget(enemy.transform);
        aimController.UpdatePosition();

        enemy.Target();

    }

    #region Score

    public void FirstToAttackP() {

        enemy.FinishRound();
        player.FinishRound();

        if (gameState != GameState.Wait)
        {

            delay = 1f;
            if (isCrRunning)
                StopAllCoroutines();

            switch (gameState)
            {

                case GameState.Prep:

                    FirstToAttackE();
                    gameplayScreen.ShowInfo("TOO EARLY");

                    break;
                case GameState.Shot:

                    gameState = GameState.Wait;

                    player.OnAttack(enemy);
                    gameplayScreen.ShowInfo(player.GetReactionTime().ToString("F3"));

                    break;

            }

        }

    }

    public void FirstToAttackE() {

        enemy.FinishRound();
        player.FinishRound();

        if (gameState != GameState.Wait)
        {

            delay = 1f;
            if (isCrRunning)
                StopAllCoroutines();

            gameState = GameState.Wait;

            enemy.OnAttack(player);
            gameplayScreen.ShowInfo(player.GetReactionTime().ToString("F3"));

        }

    }

    public void EndRound() {

        AimAt();
        gameState = GameState.Target;

    }

    #endregion

    #region Round Timer

    public void StartCountDown() {

        StartCoroutine(CountDown());
        
    }

    private IEnumerator CountDown()
    {

        delay = 1f;
        isCrRunning = true;

        gameState = GameState.Prep;
        gameplayScreen.SetTimerText(Constants.gameReady);
        yield return textRefresh;

        gameplayScreen.SetTimerText(Constants.gameSteady);

        float randomTime = Random.Range(1f, 5f);
        yield return new WaitForSeconds(randomTime);

        isCrRunning = false;

        gameState = GameState.Shot;
        gameplayScreen.SetTimerText(Constants.gameDraw);

        enemy.StartRound();
        player.StartRound();

    }

    #endregion

}
