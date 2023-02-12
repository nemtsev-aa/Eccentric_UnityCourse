using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    [Tooltip("����� �������������")]
    [SerializeField] public float LifeTime;
    [Tooltip("������")]
    [SerializeField] private GameObject canvas;
    [Tooltip("������� ������ - ����� ����������")]
    [SerializeField] private ParticleSystem _�allingDeliverer;
    [Tooltip("������� ������ - ��������� ������")]
    [SerializeField] private ParticleSystem _satisfied�ustomer;
    [Tooltip("������� ������ - ���� ������")]
    [SerializeField] private ParticleSystem _angry�ustomer;
    
    [SerializeField] private float _time;
    // Start is called before the first frame update
    void Start()
    {

        //_particle = GetComponent<ParticleSystem>();
        //_particle.Stop();
        //var particleDuration = _particle.main;
        //particleDuration.duration = 1f;

        DeactivateParticleSystem(_satisfied�ustomer, true);
        DeactivateParticleSystem(_angry�ustomer, true);

        //_satisfied�ustomer.Pause();
        //_angry�ustomer.Pause();
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

        //else
        //{
        //    float proc = _time / LifeTime;
        //    var particleDuration = _particle.main;
        //    particleDuration.duration = 1f - proc;
        //    Debug.Log("particleDuration" + particleDuration.duration);
        //}   
    }

    /// �������� �� ������� 
    private void OnEnable()
    {
        PlayerControllerX.OnSuccessfulDelivery += OnSuccessfulDelivery;
    }

    /// ������� �� ������� 
    private void OnDisable()
    {
        PlayerControllerX.OnSuccessfulDelivery -= OnSuccessfulDelivery;
    }

    ///���������� ������� "������� ��������"
    private void OnSuccessfulDelivery(PlayerControllerX player)
    {
        SuccessfulDelivery();
    }
    private void SuccessfulDelivery()
    {
        canvas.SetActive(false);
        this.GetComponent<SphereCollider>().enabled = false;

        //DeactivateParticleSystem(_�allingDeliverer, false);
        //DeactivateParticleSystem(_satisfied�ustomer, true);
        //_satisfied�ustomer.Play(); 
    }

    private void FailedDelivery()
    {
        canvas.SetActive(false);

        DeactivateParticleSystem(_�allingDeliverer, true);
        _�allingDeliverer.Stop();

        DeactivateParticleSystem(_angry�ustomer, false);
        _angry�ustomer.Play();
        Invoke(nameof(Destroy), 2f);
    }

    private void DeactivateParticleSystem(ParticleSystem particleSystem, bool status)
    {
        CFX_AutoDestructShuriken autoDestruct = particleSystem.GetComponent<CFX_AutoDestructShuriken>();
        autoDestruct.OnlyDeactivate = status;
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
    
}
