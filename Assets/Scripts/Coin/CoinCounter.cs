using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private UI_Manager uI_Manager;
    [SerializeField] private int _coinCountToWin;
    [SerializeField] private int _cointCount;

    
    /// <summary>
    /// Собрано нужное количество монет
    /// </summary>
    public static event System.Action OnWin;

    /// <summary>
    /// Устанавливаем количество монет, необходимое для победы
    /// </summary>
    /// <param name="coins"></param>
    public void SetCoinCounToWin(int coinCount)
    {
        _cointCount = 0;
        _coinCountToWin = coinCount;
        uI_Manager.ShowCoinCountValue(_cointCount, _coinCountToWin);
    }
    // Подписка на событие "Сбор монеты"
    private void OnEnable()
    {
        PlayerMove.OnCoinCollecting += CoinCounting;
    }

    // Отписка на события "Сбор монеты"
    private void OnDisable()
    {
        PlayerMove.OnCoinCollecting += CoinCounting;
    }

    /// <summary>
    /// Подсчёт количества собранных монет
    /// </summary>
    /// <param name="coin"></param>
    private void CoinCounting()
    {
        _cointCount++;
        uI_Manager.ShowCoinCountValue(_cointCount, _coinCountToWin);
        
        if (_cointCount == _coinCountToWin)
        {
            OnWin?.Invoke();
        }
    }
}
