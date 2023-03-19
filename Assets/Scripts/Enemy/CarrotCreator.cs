using UnityEngine;

public class CarrotCreator : MonoBehaviour
{
    [Tooltip ("Префаб моркови")]
    [SerializeField] private GameObject _carrotPrefab;
    [Tooltip("Точка появления моркови")]
    [SerializeField] private Transform _carrotSpawnPoint;
    // Скрипт передвижения моркови 
    private CarrotMove _carrotMove;

    public void CreateCarrot()
    {
        GameObject newCarrot = Instantiate(_carrotPrefab, _carrotSpawnPoint.position, Quaternion.identity);
        _carrotMove = newCarrot.GetComponent<CarrotMove>();
        // Устанавливаем точку появления моркови для расчёта движения
        _carrotMove.SetCarrotCreator(_carrotSpawnPoint);
    }

    public void MoveToCarrotTarget()
    {
        _carrotMove.MoveToTarget();
    }
}
