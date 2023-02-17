using UnityEngine;
using TMPro;


public class UI_Manager : MonoBehaviour
{
    [Tooltip("������ ���������")]
    [SerializeField] private GameObject _messagePanel;
    [Tooltip("������ � ����������� ������� ��������")]
    [SerializeField] private TextMeshProUGUI _sucDeliveryText;
    [Tooltip("������� ��������")]
    [SerializeField] private TextMeshProUGUI _healthValueText;
    [Tooltip("������� ������")]
    [SerializeField] private TextMeshProUGUI _chargeLevelText;
    [Tooltip("���������")]
    [SerializeField] private TextMeshProUGUI _messageText;

    // Start is called before the first frame update
    void Start()
    {
        //�������� ��������� �������
        _messagePanel.SetActive(false);
    }
    /// <summary>
    /// ���������� �������� ���������� ������� ��������
    /// </summary>
    /// <param name="sucDelivery"></param>
    /// <param name="sucDeliveryToWin"></param>
    public void ShowSuccessfulDeliveryValue(int sucDelivery, int sucDeliveryToWin)
    {
        _sucDeliveryText.text = $"{sucDelivery}/{sucDeliveryToWin}";
    }
    /// <summary>
    /// ���������� �������� ������ ������ �������
    /// </summary>
    /// <param name="chargeLevelText"></param>
    public void ShowChargeLevelValue(int chargeLevelText)
    {
        _chargeLevelText.text = $"{chargeLevelText}%";
    }
    /// <summary>
    /// ���������� �������� �������� �����
    /// </summary>
    /// <param name="chargeLevelText"></param>
    public void ShowHealthValue(int healthValue, int maxHealthValue)
    {
        _healthValueText.text = $"{healthValue}/{maxHealthValue}";
    }
    /// <summary>
    /// ��������� ������������
    /// </summary>
    /// <param name="text"></param>
    public void ShowMessage(string text)
    {
        _messagePanel.SetActive(true);
        _messageText.text = text;
    }
    /// <summary>
    /// ������ ��������� = ���������� ������ ���������
    /// </summary>
    /// <param name="text"></param>
    public void ShowMessage()
    {
        _messagePanel.SetActive(false);
    }
}
