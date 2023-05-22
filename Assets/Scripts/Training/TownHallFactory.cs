using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallFactory : AbstractFactory
{
    private Transform _spawnPoint;
    private Transform _ñollectionPoint;

    [SerializeField] private Unit _builderPrefab;
    [SerializeField] private Unit _scoutPrefab;

    /// <summary>
    /// Êîíñòðóêòîð ôàáðèêè "Ðàòóøà"
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <param name="ñollectionPoint"></param>
    public override void Setup(Transform spawnPoint, Transform ñollectionPoint) {
        _spawnPoint = spawnPoint;
        _ñollectionPoint = ñollectionPoint;
    }

    public override Unit CreateUnit1() {
        var _builder = Instantiate(_builderPrefab, _spawnPoint.position, Quaternion.identity);
        _builder.WhenClickOnGround(_ñollectionPoint.position);
        return _builder;
    }

    public override Unit CreateUnit2() {
        var _scout = Instantiate(_scoutPrefab, _spawnPoint.position, Quaternion.identity);
        _scout.WhenClickOnGround(_ñollectionPoint.position);
        return _scout;
    }

    public override Unit CreateUnit3() {
        throw new System.NotImplementedException();
    }

}
