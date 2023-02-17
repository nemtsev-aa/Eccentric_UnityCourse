using UnityEngine;
using TMPro;


public class UI_Manager : MonoBehaviour
{
    [Tooltip("Панель сообщений")]
    [SerializeField] private GameObject _messagePanel;
    [Tooltip("Запись с количеством удачных доставок")]
    [SerializeField] private TextMeshProUGUI _sucDeliveryText;
    [Tooltip("Уровень здоровья")]
    [SerializeField] private TextMeshProUGUI _healthValueText;
    [Tooltip("Уровень заряда")]
    [SerializeField] private TextMeshProUGUI _chargeLevelText;
    [Tooltip("Сообщение")]
    [SerializeField] private TextMeshProUGUI _messageText;

    // Start is called before the first frame update
    void Start()
    {
        //Скрываем финальную надпись
        _messagePanel.SetActive(false);
    }
    /// <summary>
    /// Обновление текущего количества удачных доставок
    /// </summary>
    /// <param name="sucDelivery"></param>
    /// <param name="sucDeliveryToWin"></param>
    public void ShowSuccessfulDeliveryValue(int sucDelivery, int sucDeliveryToWin)
    {
        _sucDeliveryText.text = $"{sucDelivery}/{sucDeliveryToWin}";
    }
    /// <summary>
    /// Обновление текущего уровня заряда батареи
    /// </summary>
    /// <param name="chargeLevelText"></param>
    public void ShowChargeLevelValue(int chargeLevelText)
    {
        _chargeLevelText.text = $"{chargeLevelText}%";
    }
    /// <summary>
    /// Обновление текущего здоровья дрона
    /// </summary>
    /// <param name="chargeLevelText"></param>
    public void ShowHealthValue(int healthValue, int maxHealthValue)
    {
        _healthValueText.text = $"{healthValue}/{maxHealthValue}";
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
