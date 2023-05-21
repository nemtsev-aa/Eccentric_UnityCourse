using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackFactory : AbstractFactory {

    private Transform _spawnPoint;
    private Transform _�ollectionPoint;

    [SerializeField] private Unit _standartKnightPrefab;
    [SerializeField] private Unit _lightKnightPrefab;
    [SerializeField] private Unit _heavyKnightPrefab;

    /// <summary>
    /// ����������� ������� �������
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <param name="�ollectionPoint"></param>
    public override void Setup(Transform spawnPoint, Transform �ollectionPoint) {
        _spawnPoint = spawnPoint;
        _�ollectionPoint = �ollectionPoint;
    }
   
    /// <summary>
    /// ����� �������� ������������ ������
    /// </summary>
    /// <returns></returns>
    public override Unit CreateUnit1() {
        var _standartKnight = Instantiate(_standartKnightPrefab, _spawnPoint.position, Quaternion.identity);
        _standartKnight.WhenClickOnGround(_�ollectionPoint.position);
        return _standartKnight;
    }
    /// <summary>
    /// ����� �������� ������ ������
    /// </summary>
    /// <returns></returns>
    public override Unit CreateUnit2() {
        var _lightKnight = Instantiate(_lightKnightPrefab, _spawnPoint.position, Quaternion.identity);
        _lightKnight.WhenClickOnGround(_�ollectionPoint.position);
        return _lightKnight;
    }
    /// <summary>
    /// ����� �������� ������� ������
    /// </summary>
    /// <returns></returns>
    public override Unit CreateUnit3() {
        var _heavyKnight = Instantiate(_heavyKnightPrefab, _spawnPoint.position, Quaternion.identity);
        _heavyKnight.WhenClickOnGround(_�ollectionPoint.position);
        return _heavyKnight;
    }
 
}
