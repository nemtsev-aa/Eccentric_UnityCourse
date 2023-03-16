using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotCreator : MonoBehaviour
{
    [SerializeField] private GameObject _carrotPrefab;
    [SerializeField] private Transform _carrotSpawnPoint;
    private CarrotMove _carrotMove;

    public void CreateCarrot()
    {
        GameObject newCarrot = Instantiate(_carrotPrefab, _carrotSpawnPoint.position, Quaternion.identity);
        _carrotMove = newCarrot.GetComponent<CarrotMove>();
        _carrotMove.SetCarrotCreator(_carrotSpawnPoint);
    }

    public void MoveTo—arrotTarget()
    {
        _carrotMove.MoveToTarget();
    }

}
