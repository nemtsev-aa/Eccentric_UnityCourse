using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffect : MonoBehaviour
{
    [Tooltip("����������� - ������ �� ������")]
    [SerializeField] private Image _effectImage;
    [Tooltip("���� ������� - �������")]
    [SerializeField] private Color _healColor;
    [Tooltip("���� ������� - ����")]
    [SerializeField] private Color _damageColor;
    [Tooltip("���� ������� - ���������� �������")]
    [SerializeField] private Color _timeScaleColor;

    // �������� ����������� ������ ��������� �����
    private Coroutine _animationEffect;

    public void StartDamage()
    {
        // ����������� �� ������������ ��������
        if (_animationEffect != null)
        {
            StopCoroutine(ShowEffect(_damageColor));
        }
        _animationEffect = StartCoroutine(ShowEffect(_damageColor));
    }

    public void StartHeal()
    {
        // ����������� �� ������������ ��������
        if (_animationEffect != null)
        {
            StartCoroutine(ShowEffect(_healColor));
        }
        _animationEffect = StartCoroutine(ShowEffect(_healColor));
    }

    public void StartTimeScale()
    {
        // ����������� �� ������������ ��������
        if (_animationEffect != null)
        {
            StartCoroutine(ShowEffect(_timeScaleColor));
        }
        _animationEffect = StartCoroutine(ShowEffect(_timeScaleColor));
    }

    public IEnumerator ShowEffect(Color _colorEffect)
    {
        // ������ ������ ��������� �����
        _effectImage.enabled = true;
        // ��������� �������������� �����������
        for (float i = 1; i > 0; i-=Time.deltaTime)
        {
            _effectImage.color = new Color(_colorEffect.r, _colorEffect.g, _colorEffect.b, i);

            yield return null;
        }
        _effectImage.enabled = false;
        _animationEffect = null;
    }
}
