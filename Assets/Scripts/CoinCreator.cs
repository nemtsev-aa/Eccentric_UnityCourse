using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCreator : MonoBehaviour
{
    [Tooltip ("Количество создаваемых монет")]
    [SerializeField] private int _coinCount;
    [Tooltip("Интервал создания монет")]
    [SerializeField] private float _resetCreateTime;
    [Tooltip("Зона создания монет")]
    [SerializeField] private Transform _coinCreateZone;
    [Tooltip("Префаб монеты")]
    [SerializeField] private Coin _coinPrefab;
    [Tooltip("Список созданных монет")]
    [SerializeField] private List<Coin> _coinList = new List<Coin>();

    //Время до появления следующей монеты
    private float _time;

    void Start()
    {
        /// Создаём указанное количество монет без задержки
        if (_resetCreateTime == 0)
        {
            for (int i = 0; i < _coinCount; i++)
            {
                CreateNewCoin();
            }
        }
    }

    void Update()
    {
        ///Создаём указанное количество монет с интервалом
        if (_resetCreateTime > 0)
        {
            _time += Time.deltaTime;
            if (_time >= _resetCreateTime)
            {
                _time = 0;
                CreateNewCoin();
            }
        }
    }
    /// <summary>
    /// Создание новой монеты
    /// </summary>
    private void CreateNewCoin()
    {
        if (_coinList.Count < _coinCount)
        {
             Coin newCoin = Instantiate(_coinPrefab, GetPointInCreateZone(), Quaternion.identity);
            newCoin.name = $"Coin_{_coinList.Count}";
            _coinList.Add(newCoin);
        } 
    }
    /// <summary>
    /// Получение случайной точки для создания монеты
    /// </summary>
    /// <returns></returns>
    private Vector3 GetPointInCreateZone()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _coinCreateZone.TransformPoint(x,y,z);
       
    }
    /// <summary>
    /// Визуализация зоны создания монет
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(_coinCreateZone.position, _coinCreateZone.localScale);

    }
    /// <summary>
    /// Удаление отдельной монеты из списка монет и сцены
    /// </summary>
    /// <param name="coin"></param>
    public void RemoveCoinForList(Coin coin)
    {
        if (coin != null)
        {
            _coinList.Remove(coin);
            Destroy(coin.gameObject);
        }
    }
    /// <summary>
    /// Очистка списка монет
    /// </summary>
    /// <param name="coin"></param>
    public void ClearCoinList()
    {
        foreach (var iCoin in _coinList)
        {
            RemoveCoinForList(iCoin);
        }
    }

}
