using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [Tooltip("Контейнер врагов")]
    [SerializeField] private GameObject _enemyParent;
    [Tooltip("Список врагов")]
    public List<ActivateByDistance> ListEnemyies = new List<ActivateByDistance>();
    // Расположение игрока
    public Transform _playerTransform;
    
    // Событие - убийство босса
    public event Action BossKilled;
    // Событие - убийство всех врагов
    public event Action AllEnemiesDestroyed;


    public void ShowNearEnemy()
    {
        for (int i = 0; i < ListEnemyies.Count; i++)
        {
            ListEnemyies[i].CheckDistance(_playerTransform.position);
        }
    }

    public void RemoveEnemy(EnemyHealth enemyHealth)
    {
        enemyHealth.EnemyKilled -= RemoveEnemy;
        if (enemyHealth.EnemyType == EnemyType.Bear)
            BossKilled?.Invoke();
        if (ListEnemyies.Count == 0)
            AllEnemiesDestroyed?.Invoke();

        ListEnemyies.Remove(enemyHealth.gameObject.GetComponent<ActivateByDistance>());
    }
}
