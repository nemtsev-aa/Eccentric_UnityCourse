using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : GameState
{
     
    [SerializeField] private ActionWindow _actionWindow;
    
    public override void EnterFirstTime()
    {
        base.EnterFirstTime();
        _actionWindow.Show();
    }

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
    }

    public override void Enter()
    {
        base.Enter();
        _actionWindow.Show();
    }

    public override void Exit()
    {
        base.Exit();
        _actionWindow.Hide();
    }
}
