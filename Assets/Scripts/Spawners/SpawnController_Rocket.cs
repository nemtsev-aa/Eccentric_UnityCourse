using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController_Rocket : MonoBehaviour
{
    [Tooltip("Префабы ракет")]
    [SerializeField] private Rocket[] _rockets;

    [Tooltip("Индекс активной ракеты")]
    [SerializeField] private int _rocketIndex;

    /// Подписка на событиe "Неудачная доставка"
    private void OnEnable()
    {
        Consumer.OnFailDelivery += CreateRocket;
    }

    /// Отписка от события "Неудачная доставка"
    private void OnDisable()
    {
        Consumer.OnFailDelivery -= CreateRocket;
    }

    /// <summary>
    /// Создаём новую ракету
    /// </summary>
    private void CreateRocket(Consumer consumer)
    {
        //Выбираем ракету из массива
        Rocket nextRocketPrefab = СhoosingSupplier();
        //Создаём поставщика из префаба
        Rocket nextRocket = Instantiate(nextRocketPrefab, consumer.transform.position, consumer.transform.rotation);
    }
    /// <summary>
    /// Выбор префаба следующей ракеты
    /// </summary>
    /// <param name="deliveryIndex"></param>
    private Rocket СhoosingSupplier()
    {
        //(Для наставника) Не вспомнил как определить индекс следующего элемента массива через %

        //Индекс следующей ракеты
        _rocketIndex++;
        if (_rocketIndex >= _rockets.Length)
        {
            _rocketIndex = 0;
        }
        //Префаб следующей ракеты
        Rocket nextRocket = _rockets[_rocketIndex];
        return nextRocket;
    }

}
