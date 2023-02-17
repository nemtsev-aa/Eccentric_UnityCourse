using UnityEngine;

public class DeliveryCounter : MonoBehaviour
{
    [Tooltip("UI ��������")]
    [SerializeField] private UI_Manager _uiManager;

    //���������� �������� ��� ������
    private int _successfulDeliveryToWin;
    //���������� ������� ��������
    private int _successfulDeliveryCount;
    
    /// <summary>
    /// ������
    /// </summary>
    public static event System.Action OnWin;

    /// <summary>
    /// ��������� �������� ������� �������� ����������� ��� ������
    /// </summary>
    /// <param name="value"></param>
    public void SetSuccessfulDeliveryToWin(int value)
    {
        _successfulDeliveryCount = 0;
        _successfulDeliveryToWin = value;
        _uiManager.ShowSuccessfulDeliveryValue(_successfulDeliveryCount, _successfulDeliveryToWin);
    }
    /// �������� �� ������� "������� ��������"
    private void OnEnable()
    {
        Consumer.OnSuccessfulDelivery += SuccessfulDelivery;
    }

    /// ������� �� ������� "������� ��������"
    private void OnDisable()
    {
        Consumer.OnSuccessfulDelivery -= SuccessfulDelivery;
    }

    /// ���������� ������� "������� ��������"
    private void SuccessfulDelivery(Consumer consumer)
    {
        _successfulDeliveryCount++;
        
        _uiManager.ShowSuccessfulDeliveryValue(_successfulDeliveryCount, _successfulDeliveryToWin);

        if (_successfulDeliveryCount == _successfulDeliveryToWin)
        {
            OnWin?.Invoke();
        }
    }
}
