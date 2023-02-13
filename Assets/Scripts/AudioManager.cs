using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Источник звука")]
    [SerializeField] private AudioSource _audioSource;
    [Tooltip("Звук зарядки")]
    [SerializeField] private AudioClip _chargingSound;
    [Tooltip("Звук удачной доставки")]
    [SerializeField] private AudioClip _successfulSound;
    [Tooltip("Звук касания поставщика")]
    [SerializeField] private AudioClip _onSupplierSound;
    [Tooltip("Звук низкой зарядки")]
    [SerializeField] private AudioClip _ranOutEnergySound;
    [Tooltip("Звук падения")]
    [SerializeField] private AudioClip _explodeSound;
    [Tooltip("Звук неудачного окончания игры")]
    [SerializeField] private AudioClip _fallingSound;
    [Tooltip("Звук удачного окончания игры")]
    [SerializeField] private AudioClip _winSound;

    /// Подписка на события 
    private void OnEnable()
    {
        Dron_Controller.OnSupplierPic += OnSupplierPic;
        Dron_Controller.OnPowerBankPic += OnPowerBankPic;
        Dron_Controller.Falling += Falling;
        Dron_Controller.Warning += Warning;
        Consumer.OnSuccessfulDelivery += OnSuccessfulDelivery;
        GameManager.OnWins += Wins;
    }

    /// Отписка от события 
    private void OnDisable()
    {
        Dron_Controller.OnSupplierPic -= OnSupplierPic;
        Dron_Controller.OnPowerBankPic -= OnPowerBankPic;
        Dron_Controller.Falling -= Falling;
        Dron_Controller.Warning -= Warning;
        Consumer.OnSuccessfulDelivery -= OnSuccessfulDelivery;
        GameManager.OnWins -= Wins;
    }

    ///Обработчик события "Взаимодействие игрока с поставщиком"
    private void OnSupplierPic()
    {
        _audioSource.PlayOneShot(_onSupplierSound, 1f);
    }
    ///Обработчик события "Удачная доставка"
    private void OnSuccessfulDelivery()
    {
        _audioSource.PlayOneShot(_successfulSound, 1f);
    }
    ///Обработчик события "Взаимодействие игрока с зарядным устройством"
    private void OnPowerBankPic()
    {
        _audioSource.PlayOneShot(_chargingSound, 1f);
    }
    ///Обработчик события "Поражение"
    private void Falling()
    {
        _audioSource.PlayOneShot(_fallingSound, 1f);
    }
    ///Обработчик события "Низкий заряд батареи игрока"
    private void Warning()
    {
        _audioSource.PlayOneShot(_ranOutEnergySound, 1f);
    }
    ///Обработчик события "Победа"
    private void Wins()
    {
        _audioSource.PlayOneShot(_winSound, 1f);
    }

}
