using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : GameState
{
    [Tooltip("ƒжойстик")]
    [SerializeField] private Joystick _joystick;
    [Tooltip("—крипт управл€ющий движением игрока")]
    [SerializeField] private RigidbodyMove _rigidbodyMove;
    [Tooltip("—крипт управл€ющий врагами")]
    [SerializeField] private EnemyManager _enemyManager;

    public override void EnterFirstTime()
    {
        base.EnterFirstTime();
        _enemyManager.StartNewWave(0);

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
