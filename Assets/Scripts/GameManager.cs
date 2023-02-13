using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Tooltip("���������� �������� ��� ������")]
    [SerializeField] private int _sucDeliveryToWin = 5;
    [Tooltip ("��������� ���������")]
    [SerializeField] private GameObject _finalMessageObj;
    [Tooltip ("������ � ����������� ������� ��������")]
    [SerializeField] private TextMeshProUGUI _sucDeliveryText;
    [Tooltip("������� ������")]
    [SerializeField] private TextMeshProUGUI _chargeLevelText;
    //��������� ��� ���������� ���������
    private TextMeshProUGUI _finalText;
    //���������� ������� ��������
    private int _sucDeliveryCount;
    //������� ������
    private int _chargeLevel;
    //����� ����
    private bool _gameOver;

    /// <summary>
    /// ����� ����
    /// </summary>
    public static event System.Action OnGameOver;
    /// <summary>
    /// ����� ����
    /// </summary>
    public static event System.Action OnWins;
    
    private void Awake()
    {
        //����������� �������� ����������
        _finalText = _finalMessageObj.GetComponentInChildren<TextMeshProUGUI>();
        //�������� ������
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        //�������� ��������� �������
        _finalMessageObj.SetActive(false);
        //������� ����
        _sucDeliveryText.text = _sucDeliveryCount.ToString() + "/" + _sucDeliveryToWin;
    }
   
    void Update()
    {
        //���� ���� �� ���������
        if (!_gameOver)
        {
            //�������� ������ � ������ �������
            _chargeLevel = PlayerPrefs.GetInt("chargeLevel");
            if (_chargeLevel > 0)
            {
                _chargeLevelText.text = _chargeLevel.ToString() + "%";
            }
        }  
    }
    
    /// �������� �� ������� "������� ��������"
    private void OnEnable()
    {
        Consumer.OnSuccessfulDelivery += SuccessfulDeliveryPic;
        Dron_Controller.Falling += GameOver;
    }

    /// ������� �� ������� "������� ��������"
    private void OnDisable()
    {
        Consumer.OnSuccessfulDelivery -= SuccessfulDeliveryPic;
        Dron_Controller.Falling -= GameOver;
    }

    /// ���������� ������� "������� ��������"
    private void SuccessfulDeliveryPic()
    {
        _sucDeliveryCount++;
        _sucDeliveryText.text = _sucDeliveryCount.ToString() + "/" + _sucDeliveryToWin;

        if (_sucDeliveryCount == _sucDeliveryToWin)
        {
            OnGameOver?.Invoke();
            OnWins?.Invoke();
            _finalMessageObj.SetActive(true);
            _finalText.text = "�����!";
            _gameOver = true;
        }
    }
    /// ���������� ������� "����� ����"
    private void GameOver()
    {
        _chargeLevelText.text = "0%";
        OnGameOver?.Invoke();
        _finalMessageObj.SetActive(true);
        _finalText.text = "���� ���������!";
        _gameOver = true;
    }
}
