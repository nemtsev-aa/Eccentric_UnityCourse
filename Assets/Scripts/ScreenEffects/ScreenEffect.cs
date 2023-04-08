using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffect : MonoBehaviour
{
    [Tooltip("Изображение - эффект на экране")]
    [SerializeField] private Image _effectImage;
    [Tooltip("Цвет эффекта - лечение")]
    [SerializeField] private Color _healColor;
    [Tooltip("Цвет эффекта - урон")]
    [SerializeField] private Color _damageColor;
    [Tooltip("Цвет эффекта - замедление времени")]
    [SerializeField] private Color _timeScaleColor;

    // Корутина реализующая эффект получения урона
    private Coroutine _animationEffect;

    public void StartDamage()
    {
        // Избавляемся от дублирования корутины
        if (_animationEffect != null)
        {
            StopCoroutine(ShowEffect(_damageColor));
        }
        _animationEffect = StartCoroutine(ShowEffect(_damageColor));
    }

    public void StartHeal()
    {
        // Избавляемся от дублирования корутины
        if (_animationEffect != null)
        {
            StartCoroutine(ShowEffect(_healColor));
        }
        _animationEffect = StartCoroutine(ShowEffect(_healColor));
    }

    public void StartTimeScale()
    {
        // Избавляемся от дублирования корутины
        if (_animationEffect != null)
        {
            StartCoroutine(ShowEffect(_timeScaleColor));
        }
        _animationEffect = StartCoroutine(ShowEffect(_timeScaleColor));
    }

    public IEnumerator ShowEffect(Color _colorEffect)
    {
        // Создаём эффект получения урона
        _effectImage.enabled = true;
        // Уменьшаем непрозрачность изображения
        for (float i = 1; i > 0; i-=Time.deltaTime)
        {
            _effectImage.color = new Color(_colorEffect.r, _colorEffect.g, _colorEffect.b, i);

            yield return null;
        }
        _effectImage.enabled = false;
        _animationEffect = null;
    }
}
