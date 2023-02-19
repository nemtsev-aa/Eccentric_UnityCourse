using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private UI_Manager uI_Manager;
    [SerializeField] private int _coinCountToWin;
    [SerializeField] private int _cointCount;

    
    /// <summary>
    /// ������� ������ ���������� �����
    /// </summary>
    public static event System.Action OnWin;

    /// <summary>
    /// ������������� ���������� �����, ����������� ��� ������
    /// </summary>
    /// <param name="coins"></param>
    public void SetCoinCounToWin(int coinCount)
    {
        _cointCount = 0;
        _coinCountToWin = coinCount;
        uI_Manager.ShowCoinCountValue(_cointCount, _coinCountToWin);
    }
    // �������� �� ������� "���� ������"
    private void OnEnable()
    {
        PlayerMove.OnCoinCollecting += CoinCounting;
    }

    // ������� �� ������� "���� ������"
    private void OnDisable()
    {
        PlayerMove.OnCoinCollecting += CoinCounting;
    }

    /// <summary>
    /// ������� ���������� ��������� �����
    /// </summary>
    /// <param name="coin"></param>
    private void CoinCounting()
    {
        _cointCount++;
        uI_Manager.ShowCoinCountValue(_cointCount, _coinCountToWin);
        
        if (_cointCount == _coinCountToWin)
        {
            OnWin?.Invoke();
        }
    }
}
