using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmFactory : AbstractFactory
{
    private Transform _spawnPoint;
    private Transform _ñollectionPoint;

    [SerializeField] private Unit _farmerPrefab;

    /// <summary>
    /// Êîíñòğóêòîğ ôàáğèêè "Ğàòóøà"
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <param name="ñollectionPoint"></param>
    public override void Setup(Transform spawnPoint, Transform ñollectionPoint) {
        _spawnPoint = spawnPoint;
        _ñollectionPoint = ñollectionPoint;
    }

    public override Unit CreateUnit1() {
        var _farmer = Instantiate(_farmerPrefab, _spawnPoint.position, Quaternion.identity);
        _farmer.WhenClickOnGround(_ñollectionPoint.position);
        return _farmer;
    }

    public override Unit CreateUnit2() {
        throw new System.NotImplementedException();
    }

    public override Unit CreateUnit3() {
        throw new System.NotImplementedException();
    }
}
