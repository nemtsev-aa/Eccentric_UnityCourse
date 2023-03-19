using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreen : MonoBehaviour
{
    [Tooltip("����������� - ������ ��������� �����")]
    [SerializeField] private Image _damageImage;
    // �������� ����������� ������ ��������� �����
    private Coroutine _damageEffect;

    public void StartDamage()
    {
        // ����������� �� ������������ ��������
        if (_damageEffect != null)
        {
            StopCoroutine(_damageEffect);
        }
        _damageEffect = StartCoroutine(ShowEffect());
    }

    public IEnumerator ShowEffect()
    {
        // ������ ������ ��������� �����
        _damageImage.enabled = true;
        // ��������� �������������� �����������
        for (float i = 1; i > 0; i-=Time.deltaTime)
        {
            _damageImage.color = new Color(1, 0, 0, i);
            yield return null;
        }
        _damageImage.enabled = false;
        _damageEffect = null;
    }
}
