using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealScreen : MonoBehaviour
{
    [Tooltip("����������� - ������ ������� �����")]
    [SerializeField] private Image _healImage;
    // �������� ����������� ������ ������� �����
    private Coroutine _healEffect;
    public void StartHeal()
    {
        // ����������� �� ������������ ��������
        if (_healEffect != null)
        {
            StartCoroutine(ShowEffect());
        }
        _healEffect = StartCoroutine(ShowEffect());
    }

    public IEnumerator ShowEffect()
    {
        // ������ ������ ������� ���������
        _healImage.enabled = true;
        // ��������� �������������� �����������
        for (float i = 0.5f; i > 0; i-=Time.deltaTime)
        {
            _healImage.color = new Color(0, 1, 0, i);
            yield return null;
        }
        _healImage.enabled = false;
        _healEffect = null;
    }
}
