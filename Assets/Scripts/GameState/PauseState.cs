using UnityEngine;
using UnityEngine.UI;

public class PauseState : GameState
{
    [Tooltip("������ ��� �����������")]
    [SerializeField] private Button _resumeButton;
    [Tooltip("���� �����")]
    [SerializeField] private PauseWindow _pauseWindow;

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
        _resumeButton.onClick.AddListener(gameStateManager.SetAction);
    }

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
