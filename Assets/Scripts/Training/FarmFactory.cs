using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmFactory : AbstractFactory
{
    private Transform _spawnPoint;
    private Transform _�ollectionPoint;

    [SerializeField] private Unit _farmerPrefab;

    /// <summary>
    /// ����������� ������� "������"
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <param name="�ollectionPoint"></param>
    public override void Setup(Transform spawnPoint, Transform �ollectionPoint) {
        _spawnPoint = spawnPoint;
        _�ollectionPoint = �ollectionPoint;
    }

    public override Unit CreateUnit1() {
        var _farmer = Instantiate(_farmerPrefab, _spawnPoint.position, Quaternion.identity);
        _farmer.WhenClickOnGround(_�ollectionPoint.position);
        return _farmer;
    }

    public override Unit CreateUnit2() {
        throw new System.NotImplementedException();
    }

    public override Unit CreateUnit3() {
        throw new System.NotImplementedException();
    }
}
