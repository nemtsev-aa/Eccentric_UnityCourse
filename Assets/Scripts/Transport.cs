using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [Tooltip("Префаб еды")]
    [SerializeField] public Food _food;
    [Tooltip("Статус торговли")]
    [SerializeField] private bool _tradingStatus;
    [Tooltip("Скорость движения")]
    [SerializeField] private float _moveSpeed = 2000f;

    private Rigidbody _rb;
    /// <summary>
    /// Прибытие поставщика в Торговую зону
    /// </summary>
    public static event System.Action<Transport> ArriveTradingZone;
    /// <summary>
    /// Выход поставщика из Торговой зоны
    /// </summary>
    public static event System.Action ExitTradingZone;
    /// <summary>
    /// Удаление поставщика со сцены
    /// </summary>
    public static event System.Action<Transport> DestroyTrader;
    /// <summary>
    /// Время до окончания торговли
    /// </summary>
    private float _time;
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        Move(_tradingStatus);
    }
    /// <summary>
    /// Управление торговым статусом
    /// </summary>
    /// <param name="value"></param>
    public void SetTradingStatus(bool value)
    {
        _tradingStatus = value;
        _food.gameObject.SetActive(value);
    }

    private void OnEnable()
    {
        SpawnController_Сonsumer.NoСustomers += NoСustomers;
    }
    private void OnDisable()
    {
        SpawnController_Сonsumer.NoСustomers -= NoСustomers;
    }

    /// <summary>
    /// Отсутствие клиентов 
    /// </summary>
    private void NoСustomers()
    {
        SetTradingStatus(false);
    }

    /// <summary>
    /// Перемещение транспорта
    /// </summary>
    /// <param name="tradingStatus"></param>
    private void Move(bool tradingStatus)
    {
        if (tradingStatus)
        {
            _rb.drag = 2f;
        }
        else
        {
            _rb.drag = 0f;
        }
        _rb.AddForce(Vector3.right * Time.deltaTime * _moveSpeed);
    }

    /// <summary>
    /// Удаление транспорта
    /// </summary>
    /// <param name="value"></param>
    public void DestroyTransportStatus(bool value)
    {
        if (value)
        {
            DestroyTrader?.Invoke(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TradingZone>())
        {
            SetTradingStatus(true);
            ArriveTradingZone?.Invoke(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TradingZone>())
        {
            SetTradingStatus(false);

            ExitTradingZone?.Invoke();
        }
    }
}
