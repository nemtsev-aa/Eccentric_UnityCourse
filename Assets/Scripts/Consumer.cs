using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    [Tooltip("Время существования")]
    [SerializeField] public float LifeTime;
    [Tooltip("Спрайт")]
    [SerializeField] private GameObject _canvas;
    [Tooltip("Система частиц - вызов доставщика")]
    [SerializeField] private ParticleSystem _сallingDeliverer;
    [Tooltip("Система частиц - довольный клиент")]
    [SerializeField] private ParticleSystem _satisfiedСustomer;
    [Tooltip("Система частиц - злой клиент")]
    [SerializeField] private ParticleSystem _angryСustomer;
    
    private float _time;

    /// <summary>
    /// Удачная доставка
    /// </summary>
    public static event System.Action OnSuccessfulDelivery;

    // Start is called before the first frame update
    void Start()
    {
        _сallingDeliverer.Play();
        
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

        _сallingDeliverer.Stop();
        //_angryСustomer.Stop();
        _satisfiedСustomer.Play();

        Invoke(nameof(Destroy), 2f);
    }

    private void FailedDelivery()
    {
        _canvas.SetActive(false);

        _сallingDeliverer.Stop();
        //_satisfiedСustomer.Stop();

        _angryСustomer.Play();

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
