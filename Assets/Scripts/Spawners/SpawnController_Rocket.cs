using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController_Rocket : MonoBehaviour
{
    [Tooltip("Цель")]
    [SerializeField] private Dron_Controller _target;

    [Tooltip("Префабы ракет")]
    [SerializeField] private Rocket[] _rockets;

    //Индекс активной ракеты
    private int _rocketIndex;

    //Список ракет на сцене
    [SerializeField] private List<Rocket> _rocketList;

    /// Подписка на событиe "Неудачная доставка"
    private void OnEnable()
    {
        Consumer.OnFailDelivery += CreateRocket;
        Rocket.OnDetonation += RemoveRocketFromList;
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
        //Создаём ракету из префаба
        Rocket nextRocket = Instantiate(nextRocketPrefab, consumer.transform.position, consumer.transform.rotation);
        //Добавляем ракете цель
        nextRocket.SetTarget(_target);
        //Добавляем ракету в список
        _rocketList.Add(nextRocket); 
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

    /// <summary>
    /// Удаляем ракету из списка ракет
    /// </summary>
    /// <param name="rocket"></param>
    private void RemoveRocketFromList(Rocket rocket)
    {
        _rocketList.Remove(rocket);
        Destroy(rocket.gameObject);
    }

    /// <summary>
    /// Очищаем список ракет на сцене
    /// </summary>
    public void ClearRocketList()
    {
        if (_rocketList.Count > 0)
        {
            foreach (var iRocket in _rocketList)
            {
                if (iRocket != null)
                {
                    Destroy(iRocket.gameObject);
                }
            }
            _rocketList.Clear();
        }    
    }
}
