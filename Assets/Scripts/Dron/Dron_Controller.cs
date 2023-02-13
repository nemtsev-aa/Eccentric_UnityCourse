using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron_Controller : MonoBehaviour
{
    //Контейнер перевозимыx грузов
    [SerializeField] private GameObject _cargoContainer;
    //Массив перевозимых грузов
    [SerializeField] private GameObject[] _cargo;
    //Система частиц - повреждение
    [SerializeField] private ParticleSystem _explosionParticle;
    [Tooltip("Mаксимальный размер заряда батареи")]
    [SerializeField] private float _chargeMaxValue = 10f;
    [Tooltip("Текущий уровень заряда батареи")]
    [SerializeField] private float _chargeLevel;
    //Загружен/пуст
    private bool _laden = false;
    //Статус игрока
    private bool _activ = true;
    
    /// <summary>
    /// Взаимодействие игрока с поставщиком
    /// </summary>
    public static event System.Action OnSupplierPic;
    /// <summary>
    /// Взаимодействие игрока с зарядным устройством
    /// </summary>
    public static event System.Action OnPowerBankPic;
    /// <summary>
    /// Падение игрока на землю / Батарея разряжена
    /// </summary>
    public static event System.Action Falling;
    /// <summary>
    /// Низкий заряд батареи
    /// </summary>
    public static event System.Action Warning;

    private void Awake()
    {
        int cargoCount = _cargoContainer.transform.childCount;
        _cargo = new GameObject[cargoCount];

        for (int i = 0; i < _cargo.Length; i++)
        {
            GameObject iCargo = _cargoContainer.transform.GetChild(i).gameObject;
            _cargo[i] = iCargo;
        }
    }
    void Start()
    {
        _chargeLevel = _chargeMaxValue;
        _cargoContainer.SetActive(false);
    }

    void Update()
    {
        if(_activ)
        {
            BatteryChargeMonitoring();
        }     
    }
    /// <summary>
    /// Контроль заряда батареи
    /// </summary>
    private void BatteryChargeMonitoring()
    {
        if (_chargeLevel > 0)
        {
           
            _chargeLevel -= Time.deltaTime * 0.5f;
            if (_chargeLevel <= 0)
            {
                //Батарея разряжена
                Falling?.Invoke();
            }
        }

        int chargeLevelProcentage = Mathf.RoundToInt((_chargeLevel * 100) / _chargeMaxValue);
        if (chargeLevelProcentage == 30)
        {
            //Низкий заряд батареи
            Warning?.Invoke();
        }

        //Записываем данные о заряде батареи
        PlayerPrefs.SetInt("chargeLevel", chargeLevelProcentage);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            FallingToGround();
        }
        else if (other.gameObject.CompareTag("Supplier"))
        {
            OnSupplierPic?.Invoke();
            _cargoContainer.SetActive(true);
            PackageCargo(other.gameObject);
        }
        else if (other.gameObject.CompareTag("PowerBank"))
        {
            OnPowerBankPic?.Invoke();
            _chargeLevel = _chargeMaxValue;
        }
    }
    /// <summary>
    /// Захват груза
    /// </summary>
    /// <param name="Supplier"></param>
    private void PackageCargo(GameObject supplier)
    {
        //Имя груза
        string cargoName = supplier.name;
        cargoName = cargoName.Replace("_Car(Clone)", "");
        //Отображение груза
        foreach (var iCargo in _cargo)
        {
            if (iCargo.name == cargoName)
            {
                iCargo.SetActive(true);
                //Статус - загружен
                _laden = true;
            }
            else
            {
                iCargo.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Падение на землю
    /// </summary>
    private void FallingToGround()
    {
        _chargeLevel = 0;
        _explosionParticle.Play();
        Falling?.Invoke();
    }
    /// Подписка на событие "Выход поставщика из торговой зоны" 
    private void OnEnable()
    {
        Transport.ExitTradingZone += DestroyCargo;
        GameManager.OnWins += Wins;
    }
    /// Отписка от события "Выход поставщика из торговой зоны" 
    private void OnDisable()
    {
        Transport.ExitTradingZone -= DestroyCargo;
        GameManager.OnWins -= Wins;
    }
    /// <summary>
    /// Уничтожение груза
    /// </summary>
    private void DestroyCargo()
    {
        PackageCargo(new GameObject());
    }
    ///Обработчик события "Победа"
    private void Wins()
    {
        _activ = false;
    }
}
