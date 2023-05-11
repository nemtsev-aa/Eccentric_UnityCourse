using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : GameState
{
    [Tooltip("Джойстик")]
    [SerializeField] private Joystick _joystick;
    [Tooltip("Скрипт управляющий движением игрока")]
    [SerializeField] private RigidbodyMove _rigidbodyMove;
    [Tooltip("Менеджер врагов")]
    [SerializeField] private EnemyManager _enemyManager;
    [Tooltip("Менеджер опыта")]
    [SerializeField] private ExperienceManager _experienceManager;

    public override void EnterFirstTime()
    {
        base.EnterFirstTime();
        _enemyManager.StartNewWave(0);
        _experienceManager.UpLevel();
    }

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
    }

    public override void Enter()
    {
        base.Enter();
        _joystick.Activate();
        _rigidbodyMove.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        _joystick.Deactivate();
        _rigidbodyMove.enabled = false;
    }
}
