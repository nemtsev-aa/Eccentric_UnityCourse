using System.Collections.Generic;
using UnityEngine;

public class BatchPrefabCreator : MonoBehaviour
{
    [Tooltip("Префаб ореха")]
    [SerializeField] private GameObject _acornPrefab;
    [Tooltip("Точки появления орехов")]
    [SerializeField] private Transform[] _spawnPoints;

    // Список ракет
    private List<RocketMove> _rockets = new List<RocketMove>();

    [ContextMenu("Create")]
    public void Create()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            GameObject bullet = Instantiate(_acornPrefab, _spawnPoints[i].position, _spawnPoints[i].rotation);
            RocketMove rocketMove = bullet.GetComponent<RocketMove>();
            if (rocketMove)
                _rockets.Add(rocketMove);
        }
    }

    public void StartMove()
    {
        for (int i = 0; i < _rockets.Count; i++)
        {
            float ramdomMoveSpeed = Random.Range(_rockets[i].MoveSpeed * 1f, _rockets[i].MoveSpeed * 2f);
            _rockets[i].MoveSpeed = Random.Range(1, ramdomMoveSpeed);

            _rockets[i].StartMove();
        }
    }
}
