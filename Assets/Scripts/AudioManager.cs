using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("�������� �����")]
    [SerializeField] private AudioSource _audioSource;
    [Tooltip("���� �������")]
    [SerializeField] private AudioClip _chargingSound;
    [Tooltip("���� ������� ��������")]
    [SerializeField] private AudioClip _successfulSound;
    [Tooltip("���� ������� ����������")]
    [SerializeField] private AudioClip _onSupplierSound;
    [Tooltip("���� ������ �������")]
    [SerializeField] private AudioClip _ranOutEnergySound;
    [Tooltip("���� �������")]
    [SerializeField] private AudioClip _explodeSound;
    [Tooltip("���� ���������� ��������� ����")]
    [SerializeField] private AudioClip _fallingSound;
    [Tooltip("���� �������� ��������� ����")]
    [SerializeField] private AudioClip _winSound;

    /// �������� �� ������� 
    private void OnEnable()
    {
        Dron_Controller.OnSupplier += OnSupplierPic;
        Dron_Controller.OnPowerBank += OnPowerBankPic;
        Dron_Controller.OnFalling += Falling;
        Dron_Controller.OnWarning += Warning;
        Consumer.OnSuccessfulDelivery += OnSuccessfulDelivery;
        DeliveryCounter.OnWin += Wins;
    }

    /// ������� �� ������� 
    private void OnDisable()
    {
        Dron_Controller.OnSupplier -= OnSupplierPic;
        Dron_Controller.OnPowerBank -= OnPowerBankPic;
        Dron_Controller.OnFalling -= Falling;
        Dron_Controller.OnWarning -= Warning;
        Consumer.OnSuccessfulDelivery -= OnSuccessfulDelivery;
        DeliveryCounter.OnWin -= Wins;
    }

    ///���������� ������� "�������������� ������ � �����������"
    private void OnSupplierPic()
    {
        _audioSource.PlayOneShot(_onSupplierSound, 1f);
    }
    ///���������� ������� "������� ��������"
    private void OnSuccessfulDelivery(Consumer consumer)
    {
        _audioSource.PlayOneShot(_successfulSound, 1f);
    }
    ///���������� ������� "�������������� ������ � �������� �����������"
    private void OnPowerBankPic()
    {
        _audioSource.PlayOneShot(_chargingSound, 1f);
    }
    ///���������� ������� "���������"
    private void Falling()
    {
        _audioSource.PlayOneShot(_fallingSound, 1f);
    }
    ///���������� ������� "������ ����� ������� ������"
    private void Warning()
    {
        _audioSource.PlayOneShot(_ranOutEnergySound, 1f);
    }
    ///���������� ������� "������"
    private void Wins()
    {
        _audioSource.PlayOneShot(_winSound, 1f);
    }

}
