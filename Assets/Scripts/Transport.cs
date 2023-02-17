using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [Tooltip("������ ���")]
    [SerializeField] public Food _food;
    [Tooltip("������ ��������")]
    [SerializeField] private bool _tradingStatus;
    [Tooltip("�������� ��������")]
    [SerializeField] private float _moveSpeed = 2000f;

    private Rigidbody _rb;
    /// <summary>
    /// �������� ���������� � �������� ����
    /// </summary>
    public static event System.Action<Transport> ArriveTradingZone;
    /// <summary>
    /// ����� ���������� �� �������� ����
    /// </summary>
    public static event System.Action ExitTradingZone;
    /// <summary>
    /// �������� ���������� �� �����
    /// </summary>
    public static event System.Action<Transport> DestroyTrader;
    /// <summary>
    /// ����� �� ��������� ��������
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
    /// ���������� �������� ��������
    /// </summary>
    /// <param name="value"></param>
    public void SetTradingStatus(bool value)
    {
        _tradingStatus = value;
        _food.gameObject.SetActive(value);
    }

    private void OnEnable()
    {
        SpawnController_�onsumer.No�ustomers += No�ustomers;
    }
    private void OnDisable()
    {
        SpawnController_�onsumer.No�ustomers -= No�ustomers;
    }

    /// <summary>
    /// ���������� �������� 
    /// </summary>
    private void No�ustomers()
    {
        SetTradingStatus(false);
    }

    /// <summary>
    /// ����������� ����������
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
    /// �������� ����������
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
