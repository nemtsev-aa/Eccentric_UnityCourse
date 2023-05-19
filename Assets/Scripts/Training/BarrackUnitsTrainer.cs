using UnityEngine;

public class BarrackUnitsTrainer : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _�ollectionPoint;

    [SerializeField] private BarrackAbstractFactory _factory;

    public void CreateUnit(GameObject unitPrafab) {
        switch (unitPrafab.GetComponent<Knight>().KnightType) {
            case KnightType.Standart:
                CreateStandartKnight();
                break;
            case KnightType.Light:
                CreateLightKnight();
                break;
            case KnightType.Heavy:
                CreateHeavyKnight();
                break;
            default:
                break;
        }
    }

    public void CreateStandartKnight() {
        _factory.Setup(_spawnPoint, _�ollectionPoint);
        _factory.CreateStandartKnight();
    }

    public void CreateLightKnight() {
        _factory.Setup(_spawnPoint, _�ollectionPoint);
        _factory.CreateLightKnight();
    }

    public void CreateHeavyKnight() {
        _factory.Setup(_spawnPoint, _�ollectionPoint);
        _factory.CreateHeavyKnight();
    }
}
