using UnityEngine;

public class EnemyAmmoCreator : MonoBehaviour
{
    [Tooltip ("������ ������� �����")]
    [SerializeField] private GameObject _ammoPrefab;
    [Tooltip("����� ��������� �������")]
    [SerializeField] private Transform _ammoSpawnPoint;
    
    // ������ ������������ ������� 
    private CarrotMove _carrotMove;
    // ������ ������������ ������
    private RocketMove _rocketMove;

    public void CreateCarrot()
    {
        GameObject newCarrot = Instantiate(_ammoPrefab, _ammoSpawnPoint.position, Quaternion.identity);
        _carrotMove = newCarrot.GetComponent<CarrotMove>();
        // ������������� ����� ��������� ������� ��� ������� ��������
        _carrotMove.SetCarrotCreator(_ammoSpawnPoint);
    }

    public void CreateRocket()
    {
        GameObject newRocket = Instantiate(_ammoPrefab, _ammoSpawnPoint.position, _ammoSpawnPoint.rotation);
        _rocketMove = newRocket.GetComponent<RocketMove>();
        //// ������������� ����� ��������� ������� ��� ������� ��������
        //_rocketMove.SetRocketCreator(_ammoSpawnPoint);
    }

    public void StartMove()
    {
        if (_carrotMove != null)
        {
            _carrotMove.StartMove();
        }
        else if (_rocketMove != null)
        {
            _rocketMove.StartMove();
        }
    }
}
