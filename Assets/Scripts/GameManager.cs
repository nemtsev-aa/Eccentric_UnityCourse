using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [Tooltip ("��������� ���������")]
    [SerializeField] private GameObject finalText;
    [Tooltip ("������ � ���������� ������� ��������")]
    [SerializeField] private TextMeshProUGUI _sucDeliveryText;
    [Tooltip("������� ������")]
    [SerializeField] private TextMeshProUGUI _chargeLevelText;
    //���������� ������� ��������
    private int _SucDeliveryCount;
    //������� ������
    private int _chargeLevel;
    //����� ����
    private bool _gameOver;

    void Start()
    {
        //�������� ��������� �������
        finalText.SetActive(false);
        //������ ������
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //�������� ������ � ������ �������
        _chargeLevel = Mathf.RoundToInt(PlayerPrefs.GetFloat("chargeLevel"));
        if (_chargeLevel > 0)
        {
            _chargeLevelText.text = _chargeLevel.ToString();
        }
    }
    
    /// �������� �� ������� "������� ��������"
    private void OnEnable()
    {
        PlayerControllerX.OnSuccessfulDelivery += SuccessfulDeliveryPic;
        PlayerControllerX.Falling += GameOver;
    }

    /// ������� �� ������� "������� ��������"
    private void OnDisable()
    {
        PlayerControllerX.OnSuccessfulDelivery -= SuccessfulDeliveryPic;
        PlayerControllerX.Falling -= GameOver;
    }

    /// ���������� ������� "������� ��������"
    private void SuccessfulDeliveryPic(PlayerControllerX player)
    {
        _SucDeliveryCount++;
        _sucDeliveryText.text = _SucDeliveryCount.ToString();
    }
    /// ���������� ������� "����� ����"
    private void GameOver(PlayerControllerX player)
    {
        finalText.SetActive(true);
        _gameOver = true;
    }
    private void GameOver()
    {
        finalText.SetActive(true);
    }
}
