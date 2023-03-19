using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreen : MonoBehaviour
{
    [Tooltip("Изображение - эффект получения урона")]
    [SerializeField] private Image _damageImage;
    // Корутина реализующая эффект получения урона
    private Coroutine _damageEffect;

    public void StartDamage()
    {
        // Избавляемся от дублирования корутины
        if (_damageEffect != null)
        {
            StopCoroutine(_damageEffect);
        }
        _damageEffect = StartCoroutine(ShowEffect());
    }

    public IEnumerator ShowEffect()
    {
        // Создаём эффект получения урона
        _damageImage.enabled = true;
        // Уменьшаем непрозрачность изображения
        for (float i = 1; i > 0; i-=Time.deltaTime)
        {
            _damageImage.color = new Color(1, 0, 0, i);
            yield return null;
        }
        _damageImage.enabled = false;
        _damageEffect = null;
    }
}
