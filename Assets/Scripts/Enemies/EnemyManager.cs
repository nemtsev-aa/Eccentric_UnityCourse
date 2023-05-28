using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Tooltip("Положение главного здания")]
    [SerializeField] private Building _castle;
    [Tooltip("Зона создания - север")]
    [SerializeField] private Transform _northCreateZone;
    [Tooltip("Зона создания - юг")]
    [SerializeField] private Transform _southCreateZone;
    [Tooltip("Зона создания - запад")]
    [SerializeField] private Transform _westCreateZone;
    [Tooltip("Зона создания - восток")]
    [SerializeField] private Transform _eastCreateZone;
    [Tooltip("Радиус создания врагов")]
    [SerializeField] private float _creationRadius;
    [Tooltip("Данные для создания врагов")]
    [SerializeField] private ChapterSettings _chapterSettings;
    [Tooltip("Список созданных врагов")]
    [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();

    private void Start() {
        StartNewWave(0);
    }

    public void StartNewWave(int wave)
    {
        StopAllCoroutines(); // Останавливаем все запущенные корутины перед стартом новой
        for (int i = 0; i < _chapterSettings.EnemyWavesArray.Length; i++) // Проходим во всем элементам массива врагов
        {
            Enemy iEnemy = _chapterSettings.EnemyWavesArray[i].Enemy; // i-й враг
            float iEnemyNumber = _chapterSettings.EnemyWavesArray[i].NumberPerSecond[wave]; // Периодичность создания врагов в первой волне
            if (iEnemyNumber > 0) // Если создание врагов не запланировано
                StartCoroutine(CreateEnemyInSeconds(iEnemy, iEnemyNumber));
        }
    }

    private IEnumerator CreateEnemyInSeconds(Enemy enemy, float enemyPerSecond)
    {
        while (true) // Бесконечное выполнение
        {
            yield return new WaitForSeconds(1f / enemyPerSecond); // Перерыв, соответствующий каждой волне и врагу  
            Create(enemy);
        }
    }

    public void CreateInCircle(Enemy enemy)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized; // Случайная точка на единичной окружности
        Vector3 position = new Vector3(randomPoint.x, 0f, randomPoint.y) * _creationRadius + _castle.gameObject.transform.position; // Положение врага при создании
        Enemy newEnemy = Instantiate(enemy,position, Quaternion.identity); // Новый враг в расчитанном положении
        newEnemy.Init(_castle); // Передаём врагу сведения о положении замка
        newEnemy.EnemyKilled += RemoveEnemy;
        _enemyList.Add(newEnemy); // Добавляем врага в список
    }

    public void Create(Enemy enemy) {
        
        Enemy newEnemy = new();
        int[] randomDirection = RandomSort(4, 1);
        Debug.Log(randomDirection[0]);
        switch (randomDirection[0]) {
            case 0:
                newEnemy = Instantiate(enemy, GetPointInNorthCreateZone(), Quaternion.identity); // Новый враг в расчитанном положении
                break;
            case 1:
                newEnemy = Instantiate(enemy, GetPointInSouthCreateZone(), Quaternion.identity); // Новый враг в расчитанном положении
                break;
            case 2:
                newEnemy = Instantiate(enemy, GetPointInWestCreateZone(), Quaternion.identity); // Новый враг в расчитанном положении
                break;
            case 3:
                newEnemy = Instantiate(enemy, GetPointInEastCreateZone(), Quaternion.identity); // Новый враг в расчитанном положении
                break;
            default:
                break;
        }

        newEnemy.transform.LookAt(_castle.gameObject.transform);
        newEnemy.Init(_castle); // Передаём врагу сведения о положении игрока
        newEnemy.EnemyKilled += RemoveEnemy;
        _enemyList.Add(newEnemy); // Добавляем врага в список
    }

    private Vector3 GetPointInNorthCreateZone() {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _northCreateZone.TransformPoint(x, y, z);
    }

    private Vector3 GetPointInSouthCreateZone() {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _southCreateZone.TransformPoint(x, y, z);
    }

    private Vector3 GetPointInWestCreateZone() {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _westCreateZone.TransformPoint(x, y, z);
    }

    private Vector3 GetPointInEastCreateZone() {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _eastCreateZone.TransformPoint(x, y, z);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemyList.Remove(enemy); // Удаляем врага из списка
    }

    private int[] RandomSort(int length, int number) {
        int[] array = new int[length];
        for (int i = 0; i < array.Length; i++) {
            array[i] = i;
        }

        for (int i = 0; i < array.Length; i++) {
            int oldValue = array[i];
            int newIndex = Random.Range(0, array.Length);
            array[i] = array[newIndex];
            array[newIndex] = oldValue;
        }

        int[] result = new int[number];
        for (int i = 0; i < result.Length; i++) {
            result[i] = array[i];
        }

        return result;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(_castle.gameObject.transform.position, Vector3.up, _creationRadius);
    }
#endif

}
