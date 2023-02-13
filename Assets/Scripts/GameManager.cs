using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Tooltip("Количество доставок для победы")]
    [SerializeField] private int _sucDeliveryToWin = 5;
    [Tooltip ("Финальное сообщение")]
    [SerializeField] private GameObject _finalMessageObj;
    [Tooltip ("Запись с количеством удачных доставок")]
    [SerializeField] private TextMeshProUGUI _sucDeliveryText;
    [Tooltip("Уровень заряда")]
    [SerializeField] private TextMeshProUGUI _chargeLevelText;
    //Контейнер для финального сообщения
    private TextMeshProUGUI _finalText;
    //Количество удачных доставок
    private int _sucDeliveryCount;
    //Уровень заряда
    private int _chargeLevel;
    //Конец игры
    private bool _gameOver;

    /// <summary>
    /// Конец игры
    /// </summary>
    public static event System.Action OnGameOver;
    /// <summary>
    /// Конец игры
    /// </summary>
    public static event System.Action OnWins;
    
    private void Awake()
    {
        //Присваиваем значение переменной
        _finalText = _finalMessageObj.GetComponentInChildren<TextMeshProUGUI>();
        //Скрываем курсор
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        //Скрываем финальную надпись
        _finalMessageObj.SetActive(false);
        //Текущий счёт
        _sucDeliveryText.text = _sucDeliveryCount.ToString() + "/" + _sucDeliveryToWin;
    }
   
    void Update()
    {
        //Если игра не закончена
        if (!_gameOver)
        {
            //Получаем данные о заряде батареи
            _chargeLevel = PlayerPrefs.GetInt("chargeLevel");
            if (_chargeLevel > 0)
            {
                _chargeLevelText.text = _chargeLevel.ToString() + "%";
            }
        }  
    }
    
    /// Подписка на событие "Удачная доставка"
    private void OnEnable()
    {
        Consumer.OnSuccessfulDelivery += SuccessfulDeliveryPic;
        Dron_Controller.Falling += GameOver;
    }

    /// Отписка от события "Удачная доставка"
    private void OnDisable()
    {
        Consumer.OnSuccessfulDelivery -= SuccessfulDeliveryPic;
        Dron_Controller.Falling -= GameOver;
    }

    /// Обработчик события "Удачная доставка"
    private void SuccessfulDeliveryPic()
    {
        _sucDeliveryCount++;
        _sucDeliveryText.text = _sucDeliveryCount.ToString() + "/" + _sucDeliveryToWin;

        if (_sucDeliveryCount == _sucDeliveryToWin)
        {
            OnGameOver?.Invoke();
            OnWins?.Invoke();
            _finalMessageObj.SetActive(true);
            _finalText.text = "Успех!";
            _gameOver = true;
        }
    }
    /// Обработчик события "Конец игры"
    private void GameOver()
    {
        _chargeLevelText.text = "0%";
        OnGameOver?.Invoke();
        _finalMessageObj.SetActive(true);
        _finalText.text = "Игра закончена!";
        _gameOver = true;
    }
}
