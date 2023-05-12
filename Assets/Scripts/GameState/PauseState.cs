using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseState : GameState
{
    [SerializeField] private PauseWindow _pauseWindow;

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        _pauseWindow.Show();
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1f;
        _pauseWindow.Hide();
    }
}
