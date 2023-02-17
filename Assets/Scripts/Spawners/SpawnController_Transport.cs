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
    //Список поставщиков на сцене
    [SerializeField] private List<Transport> _transporterList;
    void Start()
    {
        CreateSupplier();
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
    public void CreateSupplier()
    {
        if (_transporterList.Count <= 1)
        {
            //Устанавливаем время до появления следующего постащика
            _timer = _timeSpawn;
            //Выбираем поставщика из массива
            Transport nextSellerPrefab = СhoosingSupplier();
            //Создаём поставщика из префаба
            Transport nextSeller = Instantiate(nextSellerPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
            _transporterList.Add(nextSeller);
        }
       
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
    private void OnEnable()
    {
        SpawnController_Сonsumer.NoСustomers += CreateSupplier;
        Transport.DestroyTrader += RemoveTransportFromList;
    }

    private void OnDisable()
    {
        SpawnController_Сonsumer.NoСustomers -= CreateSupplier;
        Transport.DestroyTrader -= RemoveTransportFromList;
    }

    private void RemoveTransportFromList(Transport transport)
    {
        _transporterList.Remove(transport);
        Destroy(transport.gameObject);
    }
    /// <summary>
    /// Очищаем список поставщиков находящихся на сцене
    /// </summary>
    public void ClearTransportList()
    {
        if (_transporterList.Count > 0)
        {
            foreach (var itransport in _transporterList)
            {
                if (itransport != null)
                {
                    Destroy(itransport.gameObject);
                }
            }
            _transporterList.Clear();
        }    
    }
}
