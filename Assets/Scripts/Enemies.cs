using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // Расположение игрока
    public Transform _playerTransform;

    [Tooltip("Список врагов")]
    [SerializeField] private List<EnemyAnimal> _enemyiesList = new List<EnemyAnimal>();
    
    // Событие - убийство босса
    public event Action BossKilled;
    // Событие - убийство всех врагов
    public event Action AllEnemiesDestroyed;

    private void Start()
    {
        _enemyiesList = FindObjectsOfType<EnemyAnimal>().ToList();
        for (int i = 0; i < _enemyiesList.Count; i++)
        {
            _enemyiesList[i].EnemyKilled += RemoveEnemyFromList;
            _enemyiesList[i].GetComponent<HenMove>().Setup(_playerTransform);
        }
    }
    
    public void RemoveEnemyFromList(EnemyAnimal enemyAnimal)
    {
        enemyAnimal.EnemyKilled -= RemoveEnemyFromList;
        if (_enemyiesList.Count == 0)
            AllEnemiesDestroyed?.Invoke();
        if (enemyAnimal.EnemyAnimalType == EnemyAnimalType.Bear)
            BossKilled?.Invoke();

        _enemyiesList.Remove(enemyAnimal);
    }

    public void ShowNearEnemy()
    {
        for (int i = 0; i < _enemyiesList.Count; i++)
        {
            _enemyiesList[i].CheckDistance(_playerTransform.position);
        }
    }

    public EnemyAnimal[] GetNearest(Vector3 point, int number)
    {
        _enemyiesList = _enemyiesList.OrderBy(x => Vector3.Distance(point, x.transform.position)).ToList();
        int returnNumber = Mathf.Min(number, _enemyiesList.Count);
        EnemyAnimal[] enemies = new EnemyAnimal[returnNumber];
        for (int i = 0; i < returnNumber; i++)
        {
            enemies[i] = _enemyiesList[i];
        }
        return enemies;
    }

}
