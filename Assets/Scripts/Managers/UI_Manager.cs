using UnityEngine;
using TMPro;


public class UI_Manager : MonoBehaviour
{
    [Tooltip("Таймер")]
    [SerializeField] private TimeManager _timer;
    [Tooltip("Панель сообщений")]
    [SerializeField] private GameObject _messagePanel;
    [Tooltip("Запись с количеством собранных монет")]
    [SerializeField] private TextMeshProUGUI _coinCountText;
    [Tooltip("Время игры")]
    [SerializeField] private TextMeshProUGUI _gameTimeText;
    [Tooltip("Сообщение")]
    [SerializeField] private TextMeshProUGUI _messageText;

    private float _timeUpdate;
    // Start is called before the first frame update
    void Start()
    {
        //Скрываем финальную надпись
        _messagePanel.SetActive(false);
        //Запускаем таймер
        _timer.StartTimer();

    }

    private void Update()
    {
        _timeUpdate += Time.deltaTime;
        if (_timeUpdate > 0.1f)
        {
            _timeUpdate = 0;
            ShowGameTimeValue();
        }
    }

    /// <summary>
    /// Обновление текущего количества собранных монет
    /// </summary>
    /// <param name="coinCount"></param>
    /// <param name="coinCountToWin"></param>
    public void ShowCoinCountValue(int coinCount, int coinCountToWin)
    {
        _coinCountText.text = $"{coinCount}/{coinCountToWin}";
    }
    /// <summary>
    /// Обновление времени игры
    /// </summary>
    public void ShowGameTimeValue()
    {
        string timeValue = _timer.GetTimerValue();
        _gameTimeText.text = timeValue;

    }
    
    /// <summary>
    /// Сообщение пользователю
    /// </summary>
    /// <param name="text"></param>
    public void ShowMessage(string text)
    {
        _messagePanel.SetActive(true);
        _messageText.text = text;
    }
    /// <summary>
    /// Пустое сообщение = отключение панели сообщений
    /// </summary>
    /// <param name="text"></param>
    public void ShowMessage()
    {
        _messagePanel.SetActive(false);
    }
}
