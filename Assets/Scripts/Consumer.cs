using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    [Tooltip("����� �������������")]
    [SerializeField] public float LifeTime;
    [Tooltip("������")]
    [SerializeField] private GameObject _canvas;
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
    public static event System.Action OnSuccessfulDelivery;

    // Start is called before the first frame update
    void Start()
    {
        _�allingDeliverer.Play();
        
        _time = LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            _time = LifeTime;
            FailedDelivery();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            SuccessfulDelivery();
        }  
    }
    private void SuccessfulDelivery()
    {
        _canvas.SetActive(false);
        this.GetComponent<SphereCollider>().enabled = false;
        OnSuccessfulDelivery?.Invoke();

        _�allingDeliverer.Stop();
        //_angry�ustomer.Stop();
        _satisfied�ustomer.Play();

        Invoke(nameof(Destroy), 2f);
    }

    private void FailedDelivery()
    {
        _canvas.SetActive(false);

        _�allingDeliverer.Stop();
        //_satisfied�ustomer.Stop();

        _angry�ustomer.Play();

        Invoke(nameof(Destroy), 2f);
    }

    //private void DeactivateParticleSystem(ParticleSystem particleSystem, bool status)
    //{
    //    CFX_AutoDestructShuriken autoDestruct = particleSystem.GetComponent<CFX_AutoDestructShuriken>();
    //    autoDestruct.OnlyDeactivate = status;
    //}
    private void Destroy()
    {
        Destroy(gameObject);
    }
    
}
