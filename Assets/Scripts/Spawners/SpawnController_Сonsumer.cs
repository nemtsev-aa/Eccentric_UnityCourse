using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController_Сonsumer : MonoBehaviour
{
    [Tooltip("Максимальное количество создаваемых клиентов")]
    [SerializeField] private int _сonsumerMaxCount = 3;
    [Tooltip("Префаб клиентов")]
    [SerializeField] private Consumer _сonsumerPrefab;
    [Tooltip("Контейнер клиентов")]
    [SerializeField] private GameObject _consumerContainer;
    [Tooltip("Список активных клиентов")]
    [SerializeField] private List<Consumer> _activeConsumers;
    
    /// <summary>
    /// Массив с адресами клиентов
    /// </summary>
    private Transform[] _сonsumersTransform;
    /// <summary>
    /// Нет клиентов
    /// </summary>
    public static event System.Action NoСustomers;

    void Start()
    {
        //Количество клиентов (точек появления на карте)
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

    /// <summary>
    /// Создание клиентов
    /// </summary>
    /// <param name="count"></param>
    private void CreateConsumers(int count)
    {
        if (_activeConsumers.Count <= 2)
        {
            //Cлучайные точки появления клиентов
            List<Transform> newConsumersTransform = CreateTransformArray(count);
            //Создаём клиентов
            for (int i = 0; i < count; i++)
            {
                Transform newConsumerTransform = newConsumersTransform[i];
                //Создаём клиента из префаба
                Consumer newConsumer = Instantiate(_сonsumerPrefab, newConsumerTransform.position, newConsumerTransform.rotation);
                _activeConsumers.Add(newConsumer);
            }
        }  
    }

    private List<Transform> CreateTransformArray(int count)
    {
        //Инициализируем список случайных расположений клиентов
        List<Transform> newConsumersTransform = new List<Transform>();

        do
        {
            int newConsumerIndex = Random.Range(0, _сonsumersTransform.Length);

            Transform newConsumerTransform = _сonsumersTransform[newConsumerIndex];

            if (ChoosingTransform(newConsumerTransform))
            {
                newConsumersTransform.Add(newConsumerTransform);
            }

        } while (newConsumersTransform.Count <= count);

        return newConsumersTransform;
    }
    /// <summary>
    /// Проверяем случайные точки появления клиентов
    /// </summary>
    /// <returns></returns>
    private bool ChoosingTransform(Transform newConsumerTransform)
    {
        foreach (Consumer activeConsumer in _activeConsumers)
        {
            Transform activeConsumerTransform = activeConsumer.transform;
            if (newConsumerTransform == activeConsumerTransform)
            {
                return false;
            }
        }
        return true;
    }
    
    /// Подписка на событий "Прибытие поставщика", "Выход поставщика из торговой зоны"
    private void OnEnable()
    {
        Consumer.OnSuccessfulDelivery += RemoveConsumerFromList;
        Transport.ArriveTradingZone += ArrivalSupplier;
        Transport.ExitTradingZone += ClearConsumerList;
    }

    /// Отписка от событий "Прибытие поставщика", "Выход поставщика из торговой зоны"
    private void OnDisable()
    {
        Consumer.OnSuccessfulDelivery -= RemoveConsumerFromList;
        Transport.ArriveTradingZone -= ArrivalSupplier;
        Transport.ExitTradingZone -= ClearConsumerList;
    }
    /// <summary>
    /// Прибытие поставщика
    /// </summary>
    /// <param name="transport"></param>
    private void ArrivalSupplier(Transport transport)
    {
        ///Количество клиентов
        CreateConsumers(_сonsumerMaxCount);
    }
    public void RemoveConsumerFromList(Consumer consumer)
    {
        _activeConsumers.Remove(consumer);
        Destroy(consumer.gameObject);

        if (_activeConsumers.Count == 0)
        {
            NoСustomers?.Invoke();
        }
    }
    /// <summary>
    /// Очищаем список клиентов на сцене
    /// </summary>
    public void ClearConsumerList()
    {
        if (_activeConsumers.Count > 0)
        {
            foreach (var iConsumer in _activeConsumers)
            {
                if (iConsumer != null)
                {
                    Destroy(iConsumer.gameObject);
                }
            }
            _activeConsumers.Clear();
        }
    }
}
