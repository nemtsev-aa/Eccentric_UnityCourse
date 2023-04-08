using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [Tooltip("������ ������")]
    [SerializeField] private List<EnemyAnimal> ListEnemyies = new List<EnemyAnimal>();
    // ������������ ������
    public Transform _playerTransform;
    
    // ������� - �������� �����
    public event Action BossKilled;
    // ������� - �������� ���� ������
    public event Action AllEnemiesDestroyed;

    private void Start()
    {
        ListEnemyies = FindObjectsOfType<EnemyAnimal>().ToList();
        for (int i = 0; i < ListEnemyies.Count; i++)
        {
            ListEnemyies[i].EnemyKilled += RemoveEnemyFromList;
        }
    }
    
    public void RemoveEnemyFromList(EnemyAnimal enemyAnimal)
    {
        enemyAnimal.EnemyKilled -= RemoveEnemyFromList;
        if (ListEnemyies.Count == 0)
            AllEnemiesDestroyed?.Invoke();
        if (enemyAnimal.EnemyAnimalType == EnemyAnimalType.Bear)
            BossKilled?.Invoke();

        ListEnemyies.Remove(enemyAnimal);
    }

    public void ShowNearEnemy()
    {
        for (int i = 0; i < ListEnemyies.Count; i++)
        {
            ListEnemyies[i].CheckDistance(_playerTransform.position);
        }
    }
}
