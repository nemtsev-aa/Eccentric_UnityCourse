using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [Tooltip("��������� ������")]
    [SerializeField] private GameObject _enemyParent;
    [Tooltip("������ ������")]
    public List<ActivateByDistance> ListEnemyies = new List<ActivateByDistance>();
    // ������������ ������
    public Transform _playerTransform;
    
    // ������� - �������� �����
    public event Action BossKilled;
    // ������� - �������� ���� ������
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
