using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackFactory : BarrackAbstractFactory {

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
    public override Unit CreateStandartKnight() {
        var _standartKnight = Instantiate(_standartKnightPrefab, _spawnPoint.position, Quaternion.identity);
        _standartKnight.WhenClickOnGround(_�ollectionPoint.position);
        return _standartKnight;
    }
    /// <summary>
    /// ����� �������� ������ ������
    /// </summary>
    /// <returns></returns>
    public override Unit CreateLightKnight() {
        var _lightKnight = Instantiate(_lightKnightPrefab, _spawnPoint.position, Quaternion.identity);
        _lightKnight.WhenClickOnGround(_�ollectionPoint.position);
        return _lightKnight;
    }
    /// <summary>
    /// ����� �������� ������� ������
    /// </summary>
    /// <returns></returns>
    public override Unit CreateHeavyKnight() {
        var _heavyKnight = Instantiate(_heavyKnightPrefab, _spawnPoint.position, Quaternion.identity);
        _heavyKnight.WhenClickOnGround(_�ollectionPoint.position);
        return _heavyKnight;
    }
 
}
