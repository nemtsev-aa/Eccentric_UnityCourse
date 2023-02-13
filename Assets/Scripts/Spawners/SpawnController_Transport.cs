using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController_Transport : MonoBehaviour
{
    //Точка появления транспорта
    [SerializeField] private GameObject _spawnPoint;

    [Tooltip ("Префабы поставщиков")]
    [SerializeField] private Transport[] _suppliers;

    [Tooltip("Индекс активного поставщика")]
    [SerializeField] private int _supplierIndex;
    
    [Tooltip("Задержка перед появлением нового поставщика")]
    [SerializeField] private float _timeSpawn;

    //Время до появления следующего поставщика
    private float _timer;

    //Статус генератора поставщиков
    private bool _spawnActive = true;
    void Start()
    {
        _timer = _timeSpawn;
    }

    void Update()
    {
        //Если игра не закончена
        if (_spawnActive)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                CreateSupplier();
            }
        }
        
    }
    /// Подписка на событиe "Конец игры"
    private void OnEnable()
    {
        GameManager.OnGameOver += GameOver;
    }

    /// Отписка от события "Конец игры"
    private void OnDisable()
    {
        GameManager.OnGameOver -= GameOver;
    }

    /// <summary>
    /// Создаём нового поставщика
    /// </summary>
    private void CreateSupplier()
    {
        //Устанавливаем время до появления следующего постащика
        _timer = _timeSpawn;
        //Выбираем поставщика из массива
        Transport nextSellerPrefab = СhoosingSupplier();
        //Создаём поставщика из префаба
        GameObject nextSeller = Instantiate(nextSellerPrefab.gameObject, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
    }
    /// <summary>
    /// Выбор префаба следующего поставщика
    /// </summary>
    /// <param name="deliveryIndex"></param>
    private Transport СhoosingSupplier()
    {
        //(Для наставника) Не вспомнил как определить индекс следующего элемента массива через %
        
        //Индекс следующего поставщика 
        _supplierIndex++;
        if (_supplierIndex >= _suppliers.Length)
        {
            _supplierIndex = 0;
        }
        //Префаб следующего поставщика
        Transport nextSeller = _suppliers[_supplierIndex];
        return nextSeller;
    }
    private void GameOver()
    {
        _spawnActive = false;
    }
}
