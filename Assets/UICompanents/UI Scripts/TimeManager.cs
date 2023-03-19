using UnityEngine;

public class TimeManager : MonoBehaviour
{
    
    [Tooltip("Статус таймера")]
    [SerializeField] private bool _status;
    [Tooltip("Время на уровень")]
    [SerializeField] private float _gameTime;

    private GameProcessManager _gameProcessManager;

    public delegate void GameTimeValue(float timeValue);
    public event GameTimeValue TikGameTime;

    public delegate void GameTimeOut();
    public event GameTimeOut OnGameTimeOut;

    private float _time = 0;

    private void Start()
    {
        _gameProcessManager = GameProcessManager.Instance;
    }
    
    public void SetGameTime(float gameTime)
    {
        _gameTime = gameTime;
        _time = gameTime;
    }

    public void ResumeGame()
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
                OnGameTimeOut?.Invoke();
                _time = 0;
            }
        }
    }
}
