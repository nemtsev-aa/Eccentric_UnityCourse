using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Tooltip("Текущий уровень")]
    [SerializeField] private Level _level;
    [Tooltip("Метка - название уровня")]
    [SerializeField] private TextMeshProUGUI _levelNameText;
    [Tooltip("Массив префабов для заданий")]
    [SerializeField] private ScoreElement[] _scoreElementPrefabs;
    [Tooltip("Массив созданных заданий")]
    [SerializeField] private ScoreElement[] _scoreElements;
    [Tooltip("Родительский объект для иконок")]
    [SerializeField] private Transform _itemScoreParent;
    [Tooltip("Камера")]
    [SerializeField] private Camera _camera;

    private void Awake()
    {
        if (Instance == null) 
            Instance = this;
        else 
            Destroy(gameObject);
    }

    private void Start()
    {
        _levelNameText.text = "Level: " + Progress.Instance.Level.ToString();
        int lengthTasks = _level.Tasks.Length;  // Количество задач в уровне 
        _scoreElements = new ScoreElement[lengthTasks]; // Массив задач для уровня
        for (int taskIndex = 0; taskIndex < lengthTasks; taskIndex++)   // Проходимся по всем задачам уровня
        {
            Task task = _level.Tasks[taskIndex];    // Задача
            ItemType itemType = task.ItemType;  // Тип объекта, который надо собрать
            ScoreElement currentScoreElement = FindPrefabsByType(itemType); // Ищем префаб, который соответсвует этому типу элемента
            ScoreElement newScoreElement = Instantiate(currentScoreElement, _itemScoreParent);  // Создаем новый ScoreElement
            newScoreElement.Setup(task);    // Устанавливаем значения для созданного элемента
            _scoreElements[taskIndex] = newScoreElement;    // Добавляем ScoreElement в массив
        }
    }

    private ScoreElement FindPrefabsByType(ItemType itemType)
    {
        for (int i = 0; i < _scoreElementPrefabs.Length; i++)
        {
            if (itemType == _scoreElementPrefabs[i].ItemType)
                return _scoreElementPrefabs[i];
        }
        return null;
    }

    public bool AddScore(ItemType itemType, Vector3 position, int level = 0)
    {
        for (int i = 0; i < _scoreElements.Length; i++)
        {
            if (_scoreElements[i].ItemType != itemType) continue; 
            if (_scoreElements[i].CurrentScore == 0) continue;
            if (_scoreElements[i].Level != level) continue;

            StartCoroutine(AddScoreAnimation(_scoreElements[i], position));
            return true;
        }
        return false;
    }

    IEnumerator AddScoreAnimation(ScoreElement scoreElement, Vector3 position)
    {
        GameObject icon = Instantiate(scoreElement.FlyingIconPrefab, position, Quaternion.identity);     // Создаём иконку на месте удалённого объекта
        Vector3 a = position;   // Стартовая позация иконки
        Vector3 b = position + Vector3.back * 6.5f + Vector3.down * 5f;
        Vector3 screenPoisition = new Vector3(scoreElement.IconTransform.position.x, scoreElement.IconTransform.position.y, -_camera.transform.position.z);
        Vector3 d = _camera.ScreenToWorldPoint(screenPoisition);
        Vector3 c = d + Vector3.back * 6f;

        for (float t = 0; t < 1f; t += Time.deltaTime) // Перемещаем иконку по вычисленной кривой Безье
        {
            icon.transform.position = Bezier.GetPoint(a, b, c, d, t);
            yield return null;
        }
        
        Destroy(icon.gameObject);   // Уничтожаем иконку в конце пути
        scoreElement.AddOne();  // Добавляем собранный элемент в общий счёт
        TryCheckWin();  // Проверяем условие победы
    }

    public void TryCheckWin()
    {
        for (int i = 0; i < _scoreElements.Length; i++)
            if (_scoreElements[i].CurrentScore != 0) return;
        
        GameManager.Instance.Win();
    }
}
