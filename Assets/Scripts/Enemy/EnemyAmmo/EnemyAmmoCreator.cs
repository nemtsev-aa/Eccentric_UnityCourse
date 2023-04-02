using UnityEngine;

public class EnemyAmmoCreator : MonoBehaviour
{
    [Tooltip ("Префаб патрона врага")]
    [SerializeField] private GameObject _ammoPrefab;
    [Tooltip("Точка появления патрона")]
    [SerializeField] private Transform _ammoSpawnPoint;
    
    // Скрипт передвижения моркови 
    private CarrotMove _carrotMove;
    // Скрипт передвижения ракеты
    private RocketMove _rocketMove;

    public void CreateCarrot()
    {
        GameObject newCarrot = Instantiate(_ammoPrefab, _ammoSpawnPoint.position, Quaternion.identity);
        _carrotMove = newCarrot.GetComponent<CarrotMove>();
        // Устанавливаем точку появления моркови для расчёта движения
        _carrotMove.SetCarrotCreator(_ammoSpawnPoint);
    }

    public void CreateRocket()
    {
        GameObject newRocket = Instantiate(_ammoPrefab, _ammoSpawnPoint.position, _ammoSpawnPoint.rotation);
        _rocketMove = newRocket.GetComponent<RocketMove>();
        //// Устанавливаем точку появления моркови для расчёта движения
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
