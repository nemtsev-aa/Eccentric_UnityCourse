using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Источник звука")]
    [SerializeField] private AudioSource playerAudio;
    [Tooltip("Звук работающих двигателей")] 
    [SerializeField] private AudioClip engineSound;
    [Tooltip("Звук зарядки")]
    [SerializeField] private AudioClip chargingSound;
    [Tooltip("Звук удачной доставки")]
    [SerializeField] private AudioClip successfulSound;
    [Tooltip("Звук касания поставщика")]
    [SerializeField] private AudioClip onSupplierSound;
    [Tooltip("Звук нулевой зарядки")]
    [SerializeField] private AudioClip ranOutEnergySound;
    [Tooltip("Звук падения")]
    [SerializeField] private AudioClip explodeSound;
    [Tooltip("Звук окончания игры")]
    [SerializeField] private AudioClip fallingSound;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// Подписка на события 
    private void OnEnable()
    {
        PlayerControllerX.OnSupplierPic += OnSupplierPic;
        PlayerControllerX.OnPowerBankPic += OnPowerBankPic;
        PlayerControllerX.Falling += Falling;
        PlayerControllerX.OnSuccessfulDelivery += OnSuccessfulDelivery;
    }

    /// Отписка от события 
    private void OnDisable()
    {
        PlayerControllerX.OnSupplierPic -= OnSupplierPic;
        PlayerControllerX.OnPowerBankPic -= OnPowerBankPic;
        PlayerControllerX.Falling -= Falling;
        PlayerControllerX.OnSuccessfulDelivery -= OnSuccessfulDelivery;
    }

    ///Обработчик события "Взаимодействие игрока с поставщиком"
    private void OnSupplierPic(PlayerControllerX player)
    {
        playerAudio.PlayOneShot(onSupplierSound, 1f);
    }
    ///Обработчик события "Удачная доставка"
    private void OnSuccessfulDelivery(PlayerControllerX player)
    {
        playerAudio.PlayOneShot(successfulSound, 1f);
    }
    ///Обработчик события "Взаимодействие игрока с зарядным устройством"
    private void OnPowerBankPic(PlayerControllerX player)
    {
        playerAudio.PlayOneShot(chargingSound, 1f);
    }
    ///Обработчик события "Нулевой заряд батареи игрока"
    private void Falling(PlayerControllerX player)
    {
        playerAudio.PlayOneShot(ranOutEnergySound, 1f);
    }

}
