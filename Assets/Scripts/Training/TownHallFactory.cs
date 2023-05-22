using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallFactory : AbstractFactory
{
    private Transform _spawnPoint;
    private Transform _�ollectionPoint;

    [SerializeField] private Unit _builderPrefab;
    [SerializeField] private Unit _scoutPrefab;

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
        var _builder = Instantiate(_builderPrefab, _spawnPoint.position, Quaternion.identity);
        _builder.WhenClickOnGround(_�ollectionPoint.position);
        return _builder;
    }

    public override Unit CreateUnit2() {
        var _scout = Instantiate(_scoutPrefab, _spawnPoint.position, Quaternion.identity);
        _scout.WhenClickOnGround(_�ollectionPoint.position);
        return _scout;
    }

    public override Unit CreateUnit3() {
        throw new System.NotImplementedException();
    }

}
