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
            else
            {
                RemoveEnemy(i);
            }
        }
    }

    public void RemoveEnemy(int enemyIndex)
    {
        _enemyies.RemoveAt(enemyIndex);
        if (_enemyies.Count == 0)
        {
            AllEnemiesDestroyed?.Invoke();
        }
    }
}
