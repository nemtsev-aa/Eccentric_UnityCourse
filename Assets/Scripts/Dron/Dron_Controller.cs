using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron_Controller : MonoBehaviour
{
    [Tooltip("UI менеджер")]
    [SerializeField] private UI_Manager _uiManager;

    //Контейнер перевозимыx грузов
    [SerializeField] private GameObject _cargoContainer;
    //Система частиц - разрушение
    [SerializeField] private ParticleSystem _explosionParticle;
   
    //Mаксимальная ёмкость батареи
    private float _chargeMaxValue;
    //Текущий уровень заряда батареи
    private float _chargeLevel;
    //Mаксимальное количество здоровья
    private int _healthMaxValue;
    //Текущее количество здоровья
    private int _healthValue;

    //Загружен/пуст
    private bool _laden;
    
    #region Action
    /// <summary>
    /// Взаимодействие игрока с поставщиком
    /// </summary>
    public static event System.Action OnSupplier;
    /// <summary>
    /// Взаимодействие игрока с зарядным устройством
    /// </summary>
    public static event System.Action OnPowerBank;
    /// <summary>
    /// Падение игрока на землю / Батарея разряжена
    /// </summary>
    public static event System.Action OnFalling;
    /// <summary>
    /// Низкий заряд батареи
    /// </summary>
    public static event System.Action OnWarning;
    #endregion

    private void Start()
    {
        SetStartPosition();
    }
    public void SetStartPosition()
    {
        transform.position = new Vector3(-6f, 15f, -10f);
    }   
    /// <summary>
    /// Установка занчения максимального заряда батареи
    /// </summary>
    /// <param name="value"></param>
    public void SetСhargeMaxValue(int value)
    {
        _chargeMaxValue = value;
        _chargeLevel = _chargeMaxValue;
        BatteryChargeMonitoring();
    }

    /// <summary>
    /// Установка занчения максимального количества здоровья
    /// </summary>
    /// <param name="value"></param>
    public void SetHealthMaxValue(int value)
    {
        _healthMaxValue = value;
        _healthValue = _healthMaxValue;
        _uiManager.ShowHealthValue(_healthValue, _healthMaxValue);
    }

    void Update()
    {
        BatteryChargeMonitoring();     
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
                OnFalling?.Invoke();
            }
        }
        int chargeLevelProcentage = Mathf.RoundToInt((_chargeLevel * 100) / _chargeMaxValue);
        if (chargeLevelProcentage == 30)
        {
            //Низкий заряд батареи
            OnWarning?.Invoke();
        }
        _uiManager.ShowChargeLevelValue(chargeLevelProcentage);
    }

    /// <summary>
    /// Получение урона
    /// </summary>
    private void TakDamage(Rocket rocket)
    {
        _healthValue--;
        if (_healthValue == 0)
        {
            СriticalDamage();
            
        }
        else
        {
            _uiManager.ShowHealthValue(_healthValue, _healthMaxValue);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ground>())
        {
            СriticalDamage();
        }
        else if(other.TryGetComponent(out Food food))
        {
            OnSupplier?.Invoke();
            PackageCargo(food);
        }
        else if (other.TryGetComponent(out PowerBank powerBank))
        {
            if (powerBank.GetStatus())
            {
                _chargeLevel = _chargeMaxValue;
                powerBank.SetStatus(false);
                OnPowerBank?.Invoke();
            }
        } 
    }
    
    /// <summary>
    /// Захват груза
    /// </summary>
    /// <param name="Supplier"></param>
    private void PackageCargo(Food food)
    {
        if (_cargoContainer.transform.childCount == 0)
        {
            //Создаём новую порцию еды
            GameObject newFood = Instantiate(food.gameObject, _cargoContainer.transform.position, _cargoContainer.transform.rotation);
            newFood.transform.parent = _cargoContainer.transform;
        }  
    }
    /// <summary>
    /// Критические повреждения
    /// </summary>
    private void СriticalDamage()
    {
        _chargeLevel = 0;
        BatteryChargeMonitoring();
        _uiManager.ShowHealthValue(_healthValue, _healthMaxValue);
        ParticleSystem newexplosionParticle = Instantiate(_explosionParticle, transform.position, transform.rotation);
        
        Falling();

    }
    /// <summary>
    /// Падение на землю / Нулевое здоровье / Нулевой заряд батареи
    /// </summary>
    private void Falling()
    {
        OnFalling?.Invoke();
    }

    /// Подписка на событие "Выход поставщика из торговой зоны" 
    private void OnEnable()
    {
        Rocket.OnDetonation += TakDamage;
        Transport.ExitTradingZone += DestroyCargo;
    }
    /// Отписка от события "Выход поставщика из торговой зоны" 
    private void OnDisable()
    {
        Rocket.OnDetonation -= TakDamage;
        Transport.ExitTradingZone -= DestroyCargo;
    }
    /// <summary>
    /// Уничтожение груза
    /// </summary>
    private void DestroyCargo()
    {
        if (_cargoContainer.transform.childCount > 0)
        {
            GameObject food = _cargoContainer.transform.GetChild(0).gameObject;
            Debug.Log("FoodName" + food.name);

            Destroy(food.gameObject);
            _laden = false;
        }
    }
}
