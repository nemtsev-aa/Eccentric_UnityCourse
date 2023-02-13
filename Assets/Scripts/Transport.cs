using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [Tooltip("����� ��������")]
    [SerializeField] public GameObject _tradingPoint;
    [Tooltip ("������ ��������")]
    [SerializeField] private bool _moveStatus;
    [Tooltip("������ ��������")]
    [SerializeField] private bool _tradingStatus;
    [Tooltip("����� ��������")]
    [SerializeField] public float _tradingTime;
    [Tooltip("�������� ��������")]
    [SerializeField] private float _moveSpeed = 2f;

    /// <summary>
    /// �������� ���������� � �������� ����
    /// </summary>
    public static event System.Action<Transport> ArriveTradingZone;
    /// <summary>
    /// ����� ���������� �� �������� ����
    /// </summary>
    public static event System.Action ExitTradingZone;
    /// <summary>
    /// ����� �� ��������� ��������
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
            //��������� �������� �� ����� ��������
            _moveStatus = false;
            //������������ �������� �����
            _tradingPoint.SetActive(false);
            //����������� ����� �� ��������� ��������
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
