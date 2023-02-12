using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [Tooltip ("Финальное сообщение")]
    [SerializeField] private GameObject finalText;
    [Tooltip ("Запись о количестве удачных доставок")]
    [SerializeField] private TextMeshProUGUI _sucDeliveryText;
    [Tooltip("Уровень заряда")]
    [SerializeField] private TextMeshProUGUI _chargeLevelText;
    //Количество удачных доставок
    private int _SucDeliveryCount;
    //Уровень заряда
    private int _chargeLevel;
    //Конец игры
    private bool _gameOver;

    void Start()
    {
        //Скрываем финальную надпись
        finalText.SetActive(false);
        //Прячем курсор
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Получаем данные о заряде батареи
        _chargeLevel = Mathf.RoundToInt(PlayerPrefs.GetFloat("chargeLevel"));
        if (_chargeLevel > 0)
        {
            _chargeLevelText.text = _chargeLevel.ToString();
        }
    }
    
    /// Подписка на событие "Удачная доставка"
    private void OnEnable()
    {
        PlayerControllerX.OnSuccessfulDelivery += SuccessfulDeliveryPic;
        PlayerControllerX.Falling += GameOver;
    }

    /// Отписка от события "Удачная доставка"
    private void OnDisable()
    {
        PlayerControllerX.OnSuccessfulDelivery -= SuccessfulDeliveryPic;
        PlayerControllerX.Falling -= GameOver;
    }

    /// Обработчик события "Удачная доставка"
    private void SuccessfulDeliveryPic(PlayerControllerX player)
    {
        _SucDeliveryCount++;
        _sucDeliveryText.text = _SucDeliveryCount.ToString();
    }
    /// Обработчик события "Конец игры"
    private void GameOver(PlayerControllerX player)
    {
        finalText.SetActive(true);
        _gameOver = true;
    }
    private void GameOver()
    {
        finalText.SetActive(true);
    }
}
