using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    [Tooltip("������ - �������")]
    [SerializeField] private GameObject _shopSprite;
    [Tooltip("������� ������ - ����� ����������")]
    [SerializeField] private ParticleSystem _�allingDeliverer;
    [Tooltip("������� ������ - ��������� ������")]
    [SerializeField] private ParticleSystem _satisfied�ustomer;
    [Tooltip("������� ������ - ���� ������")]
    [SerializeField] private ParticleSystem _angry�ustomer;
    
    private float _time;

    /// <summary>
    /// ������� ��������
    /// </summary>
    public static event System.Action<Consumer> OnSuccessfulDelivery;
    /// <summary>
    /// ������� ��������
    /// </summary>
    public static event System.Action<Consumer> OnFailDelivery;

    // Start is called before the first frame update
    void Start()
    {
        _�allingDeliverer.Play();
    }
    /// �������� �� ������e "�������� ����������", "����� ���������� �� �������� ����"
    private void OnEnable()
    {
        Transport.ExitTradingZone += FailedDelivery;

    }

    /// ������� �� ������� "�������� ����������", "����� ���������� �� �������� ����"
    private void OnDisable()
    {
        Transport.ExitTradingZone -= FailedDelivery;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Food food))
        {
            SuccessfulDelivery();
            food.DestoyFood();
        }  
    }
    private void SuccessfulDelivery()
    {
        _shopSprite.SetActive(false);
        //this.GetComponent<SphereCollider>().enabled = false;
        OnSuccessfulDelivery?.Invoke(this);

        _�allingDeliverer.Stop();
        _satisfied�ustomer.Play();

        Destroy(gameObject, 2f);
    }

    private void FailedDelivery()
    {
        //�������� ����������� �������� ������, ����������� ��������
        float rand = Random.Range(0, 11);
        if (rand < 4)
        {
            OnFailDelivery?.Invoke(this);
        }

        _shopSprite.SetActive(false);

        _�allingDeliverer.Stop();
        _angry�ustomer.Play();

        Destroy(gameObject, 2f);
    }  
}
