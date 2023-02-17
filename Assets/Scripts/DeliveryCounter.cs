using UnityEngine;

public class DeliveryCounter : MonoBehaviour
{
    [Tooltip("UI менеджер")]
    [SerializeField] private UI_Manager _uiManager;

    //Количество доставок для победы
    private int _successfulDeliveryToWin;
    //Количество удачных доставок
    private int _successfulDeliveryCount;
    
    /// <summary>
    /// Победа
    /// </summary>
    public static event System.Action OnWin;

    /// <summary>
    /// Установка занчения удачных доставок необходимых для победы
    /// </summary>
    /// <param name="value"></param>
    public void SetSuccessfulDeliveryToWin(int value)
    {
        _successfulDeliveryCount = 0;
        _successfulDeliveryToWin = value;
        _uiManager.ShowSuccessfulDeliveryValue(_successfulDeliveryCount, _successfulDeliveryToWin);
    }
    /// Подписка на событие "Удачная доставка"
    private void OnEnable()
    {
        Consumer.OnSuccessfulDelivery += SuccessfulDelivery;
    }

    /// Отписка от события "Удачная доставка"
    private void OnDisable()
    {
        Consumer.OnSuccessfulDelivery -= SuccessfulDelivery;
    }

    /// Обработчик события "Удачная доставка"
    private void SuccessfulDelivery(Consumer consumer)
    {
        _successfulDeliveryCount++;
        
        _uiManager.ShowSuccessfulDeliveryValue(_successfulDeliveryCount, _successfulDeliveryToWin);

        if (_successfulDeliveryCount == _successfulDeliveryToWin)
        {
            OnWin?.Invoke();
        }
    }
}
