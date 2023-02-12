using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportMove : MonoBehaviour
{
    //Точка торговли
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
    public static event System.Action<TransportMove> ArriveTradingZone;
    /// <summary>
    /// Выход поставщика из Торговой зоны
    /// </summary>
    public static event System.Action ExitTradingZone;


    private float time;
    private void Start()
    {
        _tradingPoint.SetActive(true);
        _moveStatus = true;
        time = _tradingTime;
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
            _moveStatus = false;
            _tradingPoint.SetActive(false);
            time -= Time.deltaTime;
            if (time <= 0)
            {
                ExitTradingZone?.Invoke();

                time = _tradingTime;
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
