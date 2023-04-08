using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealScreen : MonoBehaviour
{
    [Tooltip("Изображение - эффект лечения урона")]
    [SerializeField] private Image _healImage;
    // Корутина реализующая эффект лечения урона
    private Coroutine _healEffect;
    public void StartHeal()
    {
        // Избавляемся от дублирования корутины
        if (_healEffect != null)
        {
            StartCoroutine(ShowEffect());
        }
        _healEffect = StartCoroutine(ShowEffect());
    }

    public IEnumerator ShowEffect()
    {
        // Создаём эффект лечения персонажа
        _healImage.enabled = true;
        // Уменьшаем непрозрачность изображения
        for (float i = 0.5f; i > 0; i-=Time.deltaTime)
        {
            _healImage.color = new Color(0, 1, 0, i);
            yield return null;
        }
        _healImage.enabled = false;
        _healEffect = null;
    }
}
