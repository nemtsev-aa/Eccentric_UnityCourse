using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsTrainer : MonoBehaviour {
    [SerializeField] protected Transform _spawnPoint;
    [SerializeField] protected Transform _ñollectionPoint;

    [SerializeField] private AbstractFactory _factory;

    public virtual void CreateUnit(GameObject unitPrafab) {
        switch (unitPrafab.GetComponent<Unit>().UnitType) 
        {
            case UnitType.Knight:
                Knight knight = unitPrafab.GetComponent<Knight>();
                CreateKnight(knight);
                break;
            case UnitType.Farmer:
                CreateFarmer();
                break;
            case UnitType.Builder:
                CreateBuilder();
                break;
            case UnitType.Scout:
                CreateScout();
                break;
            default:
                break;
        }
    }

    #region BarrackUnits
    private void CreateKnight(Knight knight) {
        switch (knight.KnightType) {
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
        _factory.Setup(_spawnPoint, _ñollectionPoint);
        _factory.CreateUnit1();
    }

    public void CreateLightKnight() {
        _factory.Setup(_spawnPoint, _ñollectionPoint);
        _factory.CreateUnit2();
    }

    public void CreateHeavyKnight() {
        _factory.Setup(_spawnPoint, _ñollectionPoint);
        _factory.CreateUnit3();
    }
    #endregion

    #region TownHallUnits
    public void CreateBuilder() {
        _factory.Setup(_spawnPoint, _ñollectionPoint);
        _factory.CreateUnit1();
    }

    public void CreateScout() {
        _factory.Setup(_spawnPoint, _ñollectionPoint);
        _factory.CreateUnit2();
    }
    #endregion

    #region FarmUnits
    public void CreateFarmer() {
        _factory.Setup(_spawnPoint, _ñollectionPoint);
        _factory.CreateUnit1();
    }
    #endregion
}
