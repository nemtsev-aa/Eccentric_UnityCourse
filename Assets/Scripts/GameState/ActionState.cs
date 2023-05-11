using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : GameState
{
    [Tooltip("��������")]
    [SerializeField] private Joystick _joystick;
    [Tooltip("������ ����������� ��������� ������")]
    [SerializeField] private RigidbodyMove _rigidbodyMove;
    [Tooltip("������ ����������� �������")]
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
