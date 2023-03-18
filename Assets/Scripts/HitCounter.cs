using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public static HitCounter Instance;
    [Tooltip("���������� ���������")]
    public static int HitCount;

    public delegate void HitCountValue(int hitCount);
    public event HitCountValue OnHit;
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ������� ���������� ��������� �� ������
    /// </summary>
    public void HitCounting(int hitCount)
    {
        HitCount += hitCount;
        OnHit?.Invoke(HitCount);
    }
}
