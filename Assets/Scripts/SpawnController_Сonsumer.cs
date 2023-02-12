using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController_Сonsumer : MonoBehaviour
{
    [Tooltip("Префаб клиентов")]
    [SerializeField] private GameObject _сonsumerPrefab;
    [Tooltip("Контейнер клиентов")]
    [SerializeField] private GameObject _consumerContainer;
    [Tooltip("Клиенты")]
    [SerializeField] private Transform[] _сonsumersTransform;

    [SerializeField] private List<GameObject> _activConsumers;


    void Start()
    {
        //Количество клиентов
        int _consumerCount = _consumerContainer.transform.childCount;
        //Инициализируем массив клиентов
        _сonsumersTransform = new Transform[_consumerCount];
        //Заполняем массив клиентов
        for (int i = 0; i < _consumerContainer.transform.childCount; i++)
        {
            Transform iConsumer = _consumerContainer.transform.GetChild(i).transform;
            _сonsumersTransform[i] = iConsumer;
        }
    }

    void Update()
    {
      
    }
    /// <summary>
    /// Создание потребителей
    /// </summary>
    /// <param name="count"></param>
    private void CreateConsumers(int count, float LifeTime)
    {
        //Cлучайные точки появления клиентов
        Transform[] newConsumersTransform = СhoosingTransform(count);
        //Создаём клиентов
        
        for (int i = 0; i < count; i++)
        {
            Transform newConsumerTransform = newConsumersTransform[i];
            //Создаём клиента из префаба
            GameObject newConsumer = Instantiate(_сonsumerPrefab, newConsumerTransform.position, newConsumerTransform.rotation);
            newConsumer.GetComponent<Consumer>().LifeTime = LifeTime;
            _activConsumers.Add(newConsumer);
        }
    }
    /// <summary>
    /// Выбираем случайные точки появления потребителей
    /// </summary>
    /// <returns></returns>
    private Transform[] СhoosingTransform(int count)
    {
        //Инициализируем массив случайных расположений потребителей
        Transform[] consumersTransform = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            int newConsumerIndex = Random.Range(0, _сonsumersTransform.Length);
            Transform newConsumerTransform = _сonsumersTransform[newConsumerIndex];

            consumersTransform[i] = newConsumerTransform;
        }

        return consumersTransform;
    }

    /// Подписка на событиe "Прибытие поставщика"
    private void OnEnable()
    {
        TransportMove.ArriveTradingZone += ArrivalSupplier;
    }

    /// Отписка от события "Прибытие поставщика"
    private void OnDisable()
    {
        TransportMove.ArriveTradingZone -= ArrivalSupplier;
    }
    /// <summary>
    /// Прибытие поставщика
    /// </summary>
    /// <param name="transport"></param>
    private void ArrivalSupplier(TransportMove transport)
    {
        ///Количество потребителей
        int consumerCount = Random.Range(1,5);
        float tradingTime = transport._tradingTime;
        CreateConsumers(3, tradingTime);
    }
}
