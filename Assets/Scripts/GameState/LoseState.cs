using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : GameState
{
    [SerializeField] private LoseWindow _loseWindow;
    
    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        _loseWindow.Show();
    }
    public override void Exit() {
        base.Exit();
        Time.timeScale = 1f;
        _loseWindow.Continue();
    }
}
