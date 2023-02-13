using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [Tooltip("Точка торговли")]
    [SerializeField] public GameObject _tradingPoint;
    [Tooltip ("Статус движения")]
    [SerializeField] private bool _moveStatus;
    [Tooltip("Статус торговли")]
    [SerializeField] private bool _tradingStatus;
    [Tooltip("Время торговли")]
    [SerializeField] public float _tradingTime;
    [Tooltip("Скорость движения")]
    [SerializeField] private float _moveSpeed = 2f;

    /// <summary>
    /// Прибытие поставщика в Торговую зону
    /// </summary>
    public static event System.Action<Transport> ArriveTradingZone;
    /// <summary>
    /// Выход поставщика из Торговой зоны
    /// </summary>
    public static event System.Action ExitTradingZone;
    /// <summary>
    /// Время до окончания торговли
    /// </summary>
    private float _time;
    private void Start()
    {
        _tradingPoint.SetActive(true);
        _moveStatus = true;
        _time = _tradingTime;
    }
    void Update()
    {
        Move(_moveStatus);
        Trading(_tradingStatus);
    }
    private void Move(bool moveStatus)
    {
        if (moveStatus)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 0);
        }
    }
    private void Trading(bool tradingStatus)
    {
        if (tradingStatus)
        {
            //Отключаем движение на время торговли
            _moveStatus = false;
            //Деактивируем Торговую точку
            _tradingPoint.SetActive(false);
            //Расчитываем время до окончания торговли
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                ExitTradingZone?.Invoke();
                _time = _tradingTime;
                _tradingStatus = false;

                _moveStatus = true;
            }
        }
        else
        {
            _tradingPoint.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TradingZone"))
        {
            ArriveTradingZone?.Invoke(this);
            _tradingStatus = true;
            Trading(_tradingStatus);
        }
        else if (other.gameObject.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);
        }
    }
}
