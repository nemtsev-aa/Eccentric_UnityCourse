using UnityEngine;

public class BatchPrefabCreator : MonoBehaviour
{
    [Tooltip("������ �����")]
    [SerializeField] private GameObject _acornPrefab;
    [Tooltip("����� ��������� ������")]
    [SerializeField] private Transform[] _spawnPoints;

    [ContextMenu("Create")]
    public void Create()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Instantiate(_acornPrefab, _spawnPoints[i].position, _spawnPoints[i].rotation);
        }
    }
}
