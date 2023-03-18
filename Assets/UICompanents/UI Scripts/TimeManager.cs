using System;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Tooltip("Менеджер игрового процесса")]
    [SerializeField] private GameProcessManager _gameProcessManager;
    [Tooltip("Статус таймера")]
    [SerializeField] private bool _status;
    [Tooltip("Время на уровень")]
    [SerializeField] private float _gameTime;


    public delegate void GameTimeValue(float timeValue);
    public event GameTimeValue TikGameTime;

    public delegate void GameTimeOut();
    public event GameTimeOut OnGameTimeOut;

    private float _time = 0;

    private void OnEnable()
    {
        _gameProcessManager.OnPause += PauseTimer;
        _gameProcessManager.OnResume += _gameProcessManager_OnResume;
        _gameProcessManager.OnWin += StopTimer;
        _gameProcessManager.OnLose += StopTimer;
    }

    private void OnDisable()
    {
        _gameProcessManager.OnPause -= PauseTimer;
        _gameProcessManager.OnResume -= _gameProcessManager_OnResume;
        _gameProcessManager.OnWin -= StopTimer;
        _gameProcessManager.OnLose -= StopTimer;
    }

    public void SetGameTime(float gameTime)
    {
        Debug.Log("SetGameTime");
        _gameTime = gameTime;
        _time = gameTime;
    }

    private void _gameProcessManager_OnResume()
    {
        StopTimer();
    }

    public void StartTimer()
    {
        _status = true;
        
    }
    public void PauseTimer()
    {
        _status = false;
    }
    
    public void StopTimer()
    {
        _status = false;
        _time = 0;
    }

    void Update()
    {
        if (_status)
        {
            //Время с начала уровня
            _time -= Time.deltaTime;
            TikGameTime?.Invoke(_time);

            if (_time < 0)
            {
                Debug.Log("OnLose");

                OnGameTimeOut?.Invoke();
                _time = 0;
            }
        }
    }
}
