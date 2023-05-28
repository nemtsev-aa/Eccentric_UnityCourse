using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackFactory : AbstractFactory {

    private Transform _spawnPoint;
    private Transform _ñollectionPoint;
    private Management _management;

    [SerializeField] private Unit _standartKnightPrefab;
    [SerializeField] private Unit _lightKnightPrefab;
    [SerializeField] private Unit _heavyKnightPrefab;

    /// <summary>
    /// Êîíñòğóêòîğ ôàáğèêè ğûöàğåé
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <param name="ñollectionPoint"></param>
    public override void Setup(Transform spawnPoint, Transform ñollectionPoint) {
        _spawnPoint = spawnPoint;
        _ñollectionPoint = ñollectionPoint;
        _management = FindObjectOfType<Management>();
    }
   
    /// <summary>
    /// Ìåòîä ñîçäàíèÿ ñòàíäàğòíîãî ğûöàğÿ
    /// </summary>
    /// <returns></returns>
    public override Unit CreateUnit1() {
        var _standartKnight = Instantiate(_standartKnightPrefab, _spawnPoint.position, Quaternion.identity);
        _standartKnight.WhenClickOnGround(_ñollectionPoint.position, _management);
        return _standartKnight;
    }
    /// <summary>
    /// Ìåòîä ñîçäàíèÿ ë¸ãêîãî ğûöàğÿ
    /// </summary>
    /// <returns></returns>
    public override Unit CreateUnit2() {
        var _lightKnight = Instantiate(_lightKnightPrefab, _spawnPoint.position, Quaternion.identity);
        _lightKnight.WhenClickOnGround(_ñollectionPoint.position, _management);
        return _lightKnight;
    }
    /// <summary>
    /// Ìåòîä ñîçäàíèÿ òÿæ¸ëîãî ğûöàğÿ
    /// </summary>
    /// <returns></returns>
    public override Unit CreateUnit3() {
        var _heavyKnight = Instantiate(_heavyKnightPrefab, _spawnPoint.position, Quaternion.identity);
        _heavyKnight.WhenClickOnGround(_ñollectionPoint.position, _management);
        return _heavyKnight;
    }
 
}
