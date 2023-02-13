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
        Dron_Controller.OnSupplierPic += OnSupplierPic;
        Dron_Controller.OnPowerBankPic += OnPowerBankPic;
        Dron_Controller.Falling += Falling;
        Dron_Controller.Warning += Warning;
        Consumer.OnSuccessfulDelivery += OnSuccessfulDelivery;
        GameManager.OnWins += Wins;
    }

    /// ������� �� ������� 
    private void OnDisable()
    {
        Dron_Controller.OnSupplierPic -= OnSupplierPic;
        Dron_Controller.OnPowerBankPic -= OnPowerBankPic;
        Dron_Controller.Falling -= Falling;
        Dron_Controller.Warning -= Warning;
        Consumer.OnSuccessfulDelivery -= OnSuccessfulDelivery;
        GameManager.OnWins -= Wins;
    }

    ///���������� ������� "�������������� ������ � �����������"
    private void OnSupplierPic()
    {
        _audioSource.PlayOneShot(_onSupplierSound, 1f);
    }
    ///���������� ������� "������� ��������"
    private void OnSuccessfulDelivery()
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
