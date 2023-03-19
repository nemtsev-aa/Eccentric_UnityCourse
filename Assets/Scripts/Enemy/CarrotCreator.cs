using UnityEngine;

public class CarrotCreator : MonoBehaviour
{
    [Tooltip ("������ �������")]
    [SerializeField] private GameObject _carrotPrefab;
    [Tooltip("����� ��������� �������")]
    [SerializeField] private Transform _carrotSpawnPoint;
    // ������ ������������ ������� 
    private CarrotMove _carrotMove;

    public void CreateCarrot()
    {
        GameObject newCarrot = Instantiate(_carrotPrefab, _carrotSpawnPoint.position, Quaternion.identity);
        _carrotMove = newCarrot.GetComponent<CarrotMove>();
        // ������������� ����� ��������� ������� ��� ������� ��������
        _carrotMove.SetCarrotCreator(_carrotSpawnPoint);
    }

    public void MoveToCarrotTarget()
    {
        _carrotMove.MoveToTarget();
    }
}
