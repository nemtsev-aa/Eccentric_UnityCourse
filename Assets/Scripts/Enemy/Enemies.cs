using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [Tooltip("��������� ������")]
    [SerializeField] private GameObject _enemyParent;
    [Tooltip("������ ������")]
    [SerializeField] private List<GameObject> _enemyies;
    [Tooltip("���������� �� ������")]
    [SerializeField] float _distanceToPlayer;

    public event Action AllEnemiesDestroyed;
    private Transform _playerTransform;

    private void Start()
    {
        // ���� ������� - ��������
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
            // ����������� ����
            GameObject currentEnemy = _enemyies[i].gameObject;
            if (currentEnemy != null)
            {
                // ������ �� ������ �� �����
                Vector3 currentDistance = _playerTransform.position - currentEnemy.transform.position;
                // ������ �������
                float currentDistanceValue = currentDistance.sqrMagnitude;
                // ���� ���������� ������ �������������� - ���������� �����
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
