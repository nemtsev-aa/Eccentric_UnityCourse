using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreElement : MonoBehaviour
{
    [Tooltip("Текущий уровень")]
    public int Level;
    [Tooltip("Тип объекта")]
    [SerializeField] private ItemType _itemType;
    [Tooltip("Количество оставшееся для сбора")]
    [SerializeField] private int _currentScore;
    [Tooltip("Метка для отображения количества элементов для сбора")]
    [SerializeField] private TextMeshProUGUI _text;
    [Tooltip("Ссылка на картинку")]
    [SerializeField] private Transform _iconTransform;
    [Tooltip("Анимационная кривая")]
    [SerializeField] private AnimationCurve _scaleCurve;
    [Tooltip("Префаб вылетающей иконки")]
    [SerializeField] private GameObject _flyingIconPrefab;

    public Transform IconTransform => _iconTransform;
    public GameObject FlyingIconPrefab => _flyingIconPrefab;
    public int CurrentScore => _currentScore;
    public ItemType ItemType => _itemType;
    
    //Устанавливаем значения для элемента согласно заданию на уровень
    public virtual void Setup(Task task)
    {
        _currentScore = task.Number;
        _text.text = task.Number.ToString();
    }

    // Добавить один Item к счету
    [ContextMenu("AddOne")]
    public void AddOne()
    {
        _currentScore--;
        if (_currentScore < 0)
            _currentScore = 0;      

        _text.text = _currentScore.ToString();
        // Запускаем анимацию изменения счета
        StartCoroutine(AddAnimation());
        // Проверяем условия победы
        ScoreManager.Instance.TryCheckWin();
    }

    IEnumerator AddAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime * 1.8f)
        {
            float scale = _scaleCurve.Evaluate(t);
            _iconTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        _iconTransform.localScale = Vector3.one;
    }
}
