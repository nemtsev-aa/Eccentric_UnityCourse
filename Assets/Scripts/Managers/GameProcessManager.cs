using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    Start,
    Active,
    Pause,
    Lose,
    Win,
    ExtraWin
}

public class GameProcessManager : MonoBehaviour
{
    public static GameStatus CurrentGameStatus;
    [Header("Game Settings")]
    [Tooltip("���������� ��������� ����� ��� ������")]
    [SerializeField] private int _coinCountToWin = 10;
    [Tooltip("����� �� ���� �����")]
    [SerializeField] private float _gameTime = 60f;

    [Header("Managers")]
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private WinExit _winExit;
    
        [Header("Spawner")]
    [SerializeField] private CoinCreator _coinCreator;
    
    [SerializeField] private UI_Manager _uiManager;

    private void Awake()
    {
        ShowMessagePanel();
        SetManagers(true);
        CurrentGameStatus = GameStatus.Start;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    /// <summary>
    /// ���������� �������� ���������
    /// </summary>
    /// <param name="value"></param>
    private void SetManagers(bool value)
    {
        if (value)
        {
            _coinCounter.SetCoinCounToWin(_coinCountToWin);
            _timeManager.SetGameTime(_gameTime);
            _timeManager.StartTimer();
        }  
    }


    public void StartGame()
    {
        Time.timeScale = 1f;
        CurrentGameStatus = GameStatus.Active;
        ShowMessagePanel();
        SetManagers(true);
    }

    /// �������� �� ������� "������" 
    private void OnEnable()
    {
        WinExit.OnExtraWin += ExtraWin;
        CoinCounter.OnWin += GameWin;
        TimeManager.OutGameTime += GameLose;

    }

    /// ������� �� ������� "������" 
    private void OnDisable()
    {
        WinExit.OnExtraWin += ExtraWin;
        CoinCounter.OnWin -= GameWin;
        TimeManager.OutGameTime -= GameLose;
    }

    private void ShowMessagePanel(string text = "")
    {
        ///���� � ��������� ����������� �����
        if (text == "")
        {
            _uiManager.ShowMessage();
        }
        else
        {
            _uiManager.ShowMessage(text);
        }
    }

    public void GameLose()
    {
        Time.timeScale = 0;
        CurrentGameStatus = GameStatus.Lose;
        _timeManager.StopTimer();
        ShowMessagePanel("���������");
        SetManagers(true);
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        CurrentGameStatus = GameStatus.Win;
        _timeManager.StopTimer();
        ShowMessagePanel("������ �������!");
        SetManagers(true);
    }

    public void ExtraWin()
    {
        Time.timeScale = 0;
        CurrentGameStatus = GameStatus.ExtraWin;
        ShowMessagePanel("�������������� ������!");
        SetManagers(false);
    }
    
    public void PauseGame()
    {
        CurrentGameStatus = GameStatus.Pause;
        ShowMessagePanel("�����");
        Time.timeScale = 0;
    }

    public void Return()
    {
        if (CurrentGameStatus != GameStatus.Pause)
        {
            SetManagers(true);
        }

        CurrentGameStatus = GameStatus.Active;
        ShowMessagePanel();
        Time.timeScale = 1;
    }
    public void Resume()
    {
        if (CurrentGameStatus == GameStatus.Win)
        {
            CurrentGameStatus = GameStatus.Active;
            ShowMessagePanel();
            
            Time.timeScale = 1;
            
            _winExit.OnWinExit();
        }
        else
        {
            StartGame();
        }

    }

    public void Exit()
    {
        Application.Quit();
    }
}