using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Tooltip("Статус таймера")]
    [SerializeField] private bool _status;
    [Tooltip("Время на сбор монет")]
    [SerializeField] private float _gameTime;
    [Tooltip("Значение таймера (текст)")]
    [SerializeField] private string _textValue;

    /// <summary>
    /// Время игры вышло
    /// </summary>
    public static event System.Action OutGameTime;

    private float _time = 0;
    public void SetGameTime(float gameTime)
    {
        _gameTime = gameTime;
    }
    public void StartTimer()
    {
        _status = true;
        _time = 0;
    }
    public void PauseTimer()
    {
        _status = false;
    }
    public void StopTimer()
    {
        _status = false;
        _textValue = _time.ToString("0");
    }

    void Update()
    {
        if (_status)
        {
            //Время с начала уровня
            _time += Time.deltaTime;
 
            if (_time > _gameTime)
            {
                OutGameTime?.Invoke();
                _time = 0;
            }
        }
    }

    public string GetTimerValue() 
    {
        _textValue = Mathf.RoundToInt(_time).ToString();

        return $"{_textValue}/{_gameTime}" ;
    }
}
