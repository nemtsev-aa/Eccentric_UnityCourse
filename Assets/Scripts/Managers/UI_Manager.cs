using UnityEngine;
using TMPro;


public class UI_Manager : MonoBehaviour
{
    [Tooltip("������")]
    [SerializeField] private TimeManager _timer;
    [Tooltip("������ ���������")]
    [SerializeField] private GameObject _messagePanel;
    [Tooltip("������ � ����������� ��������� �����")]
    [SerializeField] private TextMeshProUGUI _coinCountText;
    [Tooltip("����� ����")]
    [SerializeField] private TextMeshProUGUI _gameTimeText;
    [Tooltip("���������")]
    [SerializeField] private TextMeshProUGUI _messageText;

    private float _timeUpdate;
    // Start is called before the first frame update
    void Start()
    {
        //�������� ��������� �������
        _messagePanel.SetActive(false);
        //��������� ������
        _timer.StartTimer();

    }

    private void Update()
    {
        _timeUpdate += Time.deltaTime;
        if (_timeUpdate > 0.1f)
        {
            _timeUpdate = 0;
            ShowGameTimeValue();
        }
    }

    /// <summary>
    /// ���������� �������� ���������� ��������� �����
    /// </summary>
    /// <param name="coinCount"></param>
    /// <param name="coinCountToWin"></param>
    public void ShowCoinCountValue(int coinCount, int coinCountToWin)
    {
        _coinCountText.text = $"{coinCount}/{coinCountToWin}";
    }
    /// <summary>
    /// ���������� ������� ����
    /// </summary>
    public void ShowGameTimeValue()
    {
        string timeValue = _timer.GetTimerValue();
        _gameTimeText.text = timeValue;

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
