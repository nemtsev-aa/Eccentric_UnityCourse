using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    //Перевозимый груз
    [SerializeField] private GameObject _cargoContainer;
    //Перевозимый груз
    [SerializeField] private GameObject[] _cargo;
    //Система частиц - замыкание/повреждение
    [SerializeField] private ParticleSystem _explosionParticle;
    [Tooltip("Mаксимальный размер заряда батареи")]
    [SerializeField] private float _chargeMaxValue = 10f;
    [Tooltip("Текущий уровень заряда батареи")]
    [SerializeField] private float _chargeLevel;

    /// <summary>
    /// Взаимодействие игрока с поставщиком
    /// </summary>
    public static event System.Action<PlayerControllerX> OnSupplierPic;
    /// <summary>
    /// Удачная доставка
    /// </summary>
    public static event System.Action<PlayerControllerX> OnSuccessfulDelivery;
    /// <summary>
    /// Взаимодействие игрока с зарядным устройством
    /// </summary>
    public static event System.Action<PlayerControllerX> OnPowerBankPic;
    /// <summary>
    /// Падение игрока на землю / Низкий заряд батареи
    /// </summary>
    public static event System.Action<PlayerControllerX> Falling;

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
                Falling?.Invoke(this);
            }
        }

        float chargeLevelProcentage = (_chargeLevel * 100) / 10;
        //Записываем данные о заряде батареи
        PlayerPrefs.SetFloat("chargeLevel", chargeLevelProcentage);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter " + other.gameObject.name);
        if (other.gameObject.CompareTag("Ground"))
        {
            _chargeLevel = 0;
            _explosionParticle.Play();
            Falling?.Invoke(this);

            Debug.Log("Game Over!");
        }
        else if (other.gameObject.CompareTag("Supplier"))
        {
            OnSupplierPic?.Invoke(this);
            _cargoContainer.SetActive(true);
            PackageCargo(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Consumer"))
        {
            OnSuccessfulDelivery?.Invoke(this);
        }
        else if (other.gameObject.CompareTag("PowerBank"))
        {
            OnPowerBankPic?.Invoke(this);
            _chargeLevel = 10f;
        }
    }
    /// <summary>
    /// Захват груза
    /// </summary>
    /// <param name="Supplier"></param>
    private void PackageCargo(GameObject supplier)
    {
        string cargoName = supplier.name;
        cargoName = cargoName.Replace("_Car(Clone)", "");

        foreach (var iCargo in _cargo)
        {
            if (iCargo.name == cargoName)
            {
                iCargo.SetActive(true);
            }
            else
            {
                iCargo.SetActive(false);
            }
        }
    }
    /// Подписка на события 
    private void OnEnable()
    {
        TransportMove.ExitTradingZone += DestroyCargo;
    }
    /// Отписка от события 
    private void OnDisable()
    {
        TransportMove.ExitTradingZone -= DestroyCargo;
    }
    /// <summary>
    /// Уничтожение груза
    /// </summary>
    private void DestroyCargo()
    {
        PackageCargo(new GameObject());
    }
    
}
