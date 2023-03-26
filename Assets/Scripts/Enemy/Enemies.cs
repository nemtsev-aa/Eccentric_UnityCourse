using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [Tooltip("Контейнер врагов")]
    [SerializeField] private GameObject _enemyParent;
    [Tooltip("Список врагов")]
    [SerializeField] private List<GameObject> _enemyies;
    [Tooltip("Расстояние до игрока")]
    [SerializeField] float _distanceToPlayer;

    // Событие - убийство босса
    public event Action BossKilled;
    // Событие - убийство всех врагов
    public event Action AllEnemiesDestroyed;

    private Transform _playerTransform;

    private void Start()
    {
        // Цель моркови - персонаж
        _playerTransform = FindObjectOfType<PlayerMove>().transform;
    }

    public void CreateEnemyList()
    {
        foreach (Transform iEnemy in _enemyParent.transform)
        {
            _enemyies.Add(iEnemy.gameObject);
            iEnemy.GetComponent<EnemyHealth>().EnemyKilled += RemoveEnemy;
        }
    }

    public void ShowNearEnemy()
    {
        float minDistance = _distanceToPlayer;
   
        for (int i = 0; i < _enemyies.Count; i++)
        {
            // Проверяемый враг
            GameObject currentEnemy = _enemyies[i].gameObject;
            if (currentEnemy != null)
            {
                // Вектор от игрока до врага
                Vector3 currentDistance = _playerTransform.position - currentEnemy.transform.position;
                // Модуль вектора
                float currentDistanceValue = currentDistance.sqrMagnitude;
                // Если расстояние меньше установленного - активируем врага
                bool visible = (currentDistanceValue <= minDistance) ? true : false;
                currentEnemy.SetActive(visible);
            } 
        }
    }

    public void RemoveEnemy(EnemyHealth enemyHealth)
    {
        enemyHealth.EnemyKilled -= RemoveEnemy;

        _enemyies.Remove(enemyHealth.gameObject);

        if (enemyHealth.EnemyType == EnemyType.Bear)
        {
            BossKilled?.Invoke();
        }
        if (_enemyies.Count == 0)
            AllEnemiesDestroyed?.Invoke();

    }
}
