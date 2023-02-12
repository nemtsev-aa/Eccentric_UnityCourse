using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("�������� �����")]
    [SerializeField] private AudioSource playerAudio;
    [Tooltip("���� ���������� ����������")] 
    [SerializeField] private AudioClip engineSound;
    [Tooltip("���� �������")]
    [SerializeField] private AudioClip chargingSound;
    [Tooltip("���� ������� ��������")]
    [SerializeField] private AudioClip successfulSound;
    [Tooltip("���� ������� ����������")]
    [SerializeField] private AudioClip onSupplierSound;
    [Tooltip("���� ������� �������")]
    [SerializeField] private AudioClip ranOutEnergySound;
    [Tooltip("���� �������")]
    [SerializeField] private AudioClip explodeSound;
    [Tooltip("���� ��������� ����")]
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

    /// �������� �� ������� 
    private void OnEnable()
    {
        PlayerControllerX.OnSupplierPic += OnSupplierPic;
        PlayerControllerX.OnPowerBankPic += OnPowerBankPic;
        PlayerControllerX.Falling += Falling;
        PlayerControllerX.OnSuccessfulDelivery += OnSuccessfulDelivery;
    }

    /// ������� �� ������� 
    private void OnDisable()
    {
        PlayerControllerX.OnSupplierPic -= OnSupplierPic;
        PlayerControllerX.OnPowerBankPic -= OnPowerBankPic;
        PlayerControllerX.Falling -= Falling;
        PlayerControllerX.OnSuccessfulDelivery -= OnSuccessfulDelivery;
    }

    ///���������� ������� "�������������� ������ � �����������"
    private void OnSupplierPic(PlayerControllerX player)
    {
        playerAudio.PlayOneShot(onSupplierSound, 1f);
    }
    ///���������� ������� "������� ��������"
    private void OnSuccessfulDelivery(PlayerControllerX player)
    {
        playerAudio.PlayOneShot(successfulSound, 1f);
    }
    ///���������� ������� "�������������� ������ � �������� �����������"
    private void OnPowerBankPic(PlayerControllerX player)
    {
        playerAudio.PlayOneShot(chargingSound, 1f);
    }
    ///���������� ������� "������� ����� ������� ������"
    private void Falling(PlayerControllerX player)
    {
        playerAudio.PlayOneShot(ranOutEnergySound, 1f);
    }

}
