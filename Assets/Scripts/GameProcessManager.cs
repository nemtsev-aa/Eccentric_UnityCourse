using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    Start,
    Active,
    Pause,
    Lose,
    Win
}

public class GameProcessManager : MonoBehaviour
{
    public static GameStatus CurrentGameStatus;
    [Header("Game Settings")]
    [Tooltip("Количество доставок для победы")]
    [SerializeField] private int _successfulDeliveryToWin;
    [Tooltip("Mаксимальная ёмкость батареи")]
    [SerializeField] private int _chargeMaxValue;
    [Tooltip("Mаксимальное количество здоровья")]
    [SerializeField] private int _healthMaxValue;
    
    [Header("Managers")]
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private DeliveryCounter _deliveryCounter;
    [SerializeField] private Dron_Controller _dron_Controller;
    [SerializeField] private List<GameObject> _managers;

    [Header("Spawners")]
    [SerializeField] private SpawnController_Transport _transports;
    [SerializeField] private SpawnController_Сonsumer _consumers;
    [SerializeField] private SpawnController_Rocket _rockets;
    [SerializeField] private List<GameObject> _spawners;

    [SerializeField] private UI_Manager _uiManager;

    private void Awake()
    {
        _managers.Add(_audioManager.gameObject);
        _managers.Add(_deliveryCounter.gameObject);
        _managers.Add(_dron_Controller.gameObject);

        _spawners.Add(_transports.gameObject);
        _spawners.Add(_consumers.gameObject);
        _spawners.Add(_rockets.gameObject);

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
    /// Управление генераторами объектов
    /// </summary>
    /// <param name="value"></param>
    private void SetSpawners(bool value)
    {
        for (int i = 0; i < _spawners.Count; i++)
        {
            ///Очищаем списки объектов, находящихся на сцене
            if (!value)
            {
                if (_spawners[i].TryGetComponent<SpawnController_Transport>(out SpawnController_Transport _transport))
                {
                    _transport.ClearTransportList();
                }
                else if (_spawners[i].TryGetComponent<SpawnController_Rocket>(out SpawnController_Rocket _rocket))
                {
                    _rocket.ClearRocketList();
                }
                else if (_spawners[i].TryGetComponent<SpawnController_Сonsumer>(out SpawnController_Сonsumer _consumer))
                {
                    _consumer.ClearConsumerList();
                }
            }
            else
            {
                if (_spawners[i].TryGetComponent<SpawnController_Transport>(out SpawnController_Transport _transport))
                {
                    _transport.CreateSupplier();
                }
            }

            _spawners[i].SetActive(value);
        }
    }

    /// <summary>
    /// Управление элементами игры
    /// </summary>
    /// <param name="value"></param>
    private void SetManagers(bool value)
    {
        if (value)
        {
            for (int i = 0; i < _managers.Count; i++)
            {
                if (_managers[i].TryGetComponent<DeliveryCounter>(out DeliveryCounter deliveryCounter))
                {
                    deliveryCounter.SetSuccessfulDeliveryToWin(_successfulDeliveryToWin);
                }
                if (_managers[i].TryGetComponent<Dron_Controller>(out Dron_Controller dronController))
                {
                    dronController.SetСhargeMaxValue(_chargeMaxValue);
                    dronController.SetHealthMaxValue(_healthMaxValue);
                    dronController.SetStartPosition();
                }
                _managers[i].SetActive(value);
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        CurrentGameStatus = GameStatus.Active;

        SetManagers(true);
        SetSpawners(true);
    }

    /// Подписка на события "Победа" / "Поражение"
    private void OnEnable()
    {
        DeliveryCounter.OnWin += GameWin;
        Dron_Controller.OnFalling += GameLose;
    }

    /// Отписка от события "Неудачная доставка"
    private void OnDisable()
    {
        DeliveryCounter.OnWin -= GameWin;
        Dron_Controller.OnFalling -= GameLose;
    }

    private void ShowMessagePanel(string text = "")
    {
        ///Если в сообщении отсутствует текст
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
        ShowMessagePanel("Поражение");
        SetManagers(false);
        SetSpawners(false);
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        CurrentGameStatus = GameStatus.Win;
        ShowMessagePanel("Победа!");
        SetManagers(false);
        SetSpawners(false);
    }

    public void PauseGame()
    {
        CurrentGameStatus = GameStatus.Pause;
        ShowMessagePanel("Пауза");
        Time.timeScale = 0;
    }

    public void Return()
    {
        if (CurrentGameStatus != GameStatus.Pause)
        {
            SetManagers(true);
            SetSpawners(true);
        }

        CurrentGameStatus = GameStatus.Active;
        ShowMessagePanel();
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}