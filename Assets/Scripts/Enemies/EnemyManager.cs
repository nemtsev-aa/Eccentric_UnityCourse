using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Tooltip("Положение игрока")]
    [SerializeField] private Transform _playerTransform;
    [Tooltip("Радиус создания врагов")]
    [SerializeField] private float _creationRadius;
    [Tooltip("Данные для создания врагов")]
    [SerializeField] private ChapterSettings _chapterSettings;
    [Tooltip("Список созданных врагов")]
    [SerializeField] private List<EnemyAnimal> _enemyList = new List<EnemyAnimal>();

    public void StartNewWave(int wave)
    {
        StopAllCoroutines(); // Останавливаем все запущенные корутины перед стартом новой
        for (int i = 0; i < _chapterSettings.EnemyWavesArray.Length; i++) // Проходим во всем элементам массива врагов
        {
            EnemyAnimal iEnemy = _chapterSettings.EnemyWavesArray[i].Enemy; // i-й враг
            float iEnemyNumber = _chapterSettings.EnemyWavesArray[i].NumberPerSecond[wave]; // Периодичность создания врагов в первой волне
            if (iEnemyNumber > 0) // Если создание врагов не запланировано
                StartCoroutine(CreateEnemyInSeconds(iEnemy, iEnemyNumber));
        }
    }

    private IEnumerator CreateEnemyInSeconds(EnemyAnimal enemy, float enemyPerSecond)
    {
        while (true) // Бесконечное выполнение
        {
            yield return new WaitForSeconds(1f / enemyPerSecond); // Перерыв, соответствующий каждой волне и врагу  
            Create(enemy);
        }
    }

    public void Create(EnemyAnimal enemy)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized; // Случайная точка на единичной окружности
        Vector3 position = new Vector3(randomPoint.x, 0f, randomPoint.y) * _creationRadius + _playerTransform.position; // Положение врага при создании
        EnemyAnimal newEnemy = Instantiate(enemy,position, Quaternion.identity); // Новый враг в расчитанном положении
        newEnemy.Init(_playerTransform); // Передаём врагу сведения о положении игрока
        newEnemy.EnemyKilled += RemoveEnemy;
        _enemyList.Add(newEnemy); // Добавляем врага в список
    }

    public void RemoveEnemy(EnemyAnimal enemy)
    {
        _enemyList.Remove(enemy); // Удаляем врага из списка
    }

    public EnemyAnimal[] GetNearest(Vector3 point, int number)
    {
        _enemyList = _enemyList.OrderBy(x => Vector3.Distance(point, x.transform.position)).ToList();
        int returnNumber = Mathf.Min(number, _enemyList.Count);
        EnemyAnimal[] enemies = new EnemyAnimal[returnNumber];
        for (int i = 0; i < returnNumber; i++)
        {
            enemies[i] = _enemyList[i];
        }
        return enemies;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.DrawWireDisc(_playerTransform.position, Vector3.up, _creationRadius);
#endif
    }


}
