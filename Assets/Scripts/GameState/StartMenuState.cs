using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuState : GameState
{
    [Tooltip("������ ��� ������")]
    [SerializeField] private Button _tabToStartButton;
    [Tooltip("���� ��������� ����")]
    [SerializeField] private GameObject _startMenuObject;

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
        _tabToStartButton.onClick.AddListener(gameStateManager.SetAction);
    }

    public override void Enter()
    {
        base.Enter();
        _startMenuObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        _startMenuObject.SetActive(false);
    }
}
