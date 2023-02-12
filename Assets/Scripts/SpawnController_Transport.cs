using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController_Transport : MonoBehaviour
{
    //Точка появления транспорта
    [SerializeField] private GameObject _spawnPoint;

    [Tooltip ("Префабы поставщиков")]
    [SerializeField] private GameObject[] _suppliers;

    [Tooltip("Индекс активного поставщика")]
    [SerializeField] private int _supplierIndex;
    
    [Tooltip("Задержка перед появлением нового поставщика")]
    [SerializeField] private float _timeSpawn;

    //Время до появления следующего поставщика
    [SerializeField] private float _timer;

    void Start()
    {
        _timer = _timeSpawn;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            CreateSupplier();
        }
    }
    /// <summary>
    /// Создаём нового поставщика
    /// </summary>
    private void CreateSupplier()
    {
        //Устанавливаем время до появления следующего постащика
        _timer = _timeSpawn;
        //Выбираем поставщика из массива
        GameObject nextSellerPrefab = СhoosingSupplier();
        //Создаём поставщика из префаба
        GameObject nextSeller = Instantiate(nextSellerPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
    }
    /// <summary>
    /// Выбор префаба следующего поставщика
    /// </summary>
    /// <param name="deliveryIndex"></param>
    private GameObject СhoosingSupplier()
    {
        //(Для наставника) Не вспомнил как определить индекс следующего элемента массива через %
        
        //Индекс следующего поставщика 
        _supplierIndex++;
        if (_supplierIndex >= _suppliers.Length)
        {
            _supplierIndex = 0;
        }
        //Префаб следующего поставщика
        GameObject nextSeller = _suppliers[_supplierIndex];
        return nextSeller;
    }
}
