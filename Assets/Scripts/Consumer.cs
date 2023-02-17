using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    [Tooltip("Спрайт - покупка")]
    [SerializeField] private GameObject _shopSprite;
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
    public static event System.Action<Consumer> OnSuccessfulDelivery;
    /// <summary>
    /// Удачная доставка
    /// </summary>
    public static event System.Action<Consumer> OnFailDelivery;

    // Start is called before the first frame update
    void Start()
    {
        _сallingDeliverer.Play();
    }
    /// Подписка на событиe "Прибытие поставщика", "Выход поставщика из торговой зоны"
    private void OnEnable()
    {
        Transport.ExitTradingZone += FailedDelivery;

    }

    /// Отписка от события "Прибытие поставщика", "Выход поставщика из торговой зоны"
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

        _сallingDeliverer.Stop();
        _satisfiedСustomer.Play();

        Destroy(gameObject, 2f);
    }

    private void FailedDelivery()
    {
        //Выявляем вероятность создания ракеты, недовольным клиентом
        float rand = Random.Range(0, 11);
        if (rand < 4)
        {
            OnFailDelivery?.Invoke(this);
        }

        _shopSprite.SetActive(false);

        _сallingDeliverer.Stop();
        _angryСustomer.Play();

        Destroy(gameObject, 2f);
    }  
}
