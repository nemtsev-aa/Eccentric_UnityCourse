using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MineEffect), menuName = "Effects/Continuous/" + nameof(MineEffect))]
public class MineEffect : ContinuousEffect
{
    [Tooltip("Префаб мины")]
    [SerializeField] private Mine _minePrefab;
    [SerializeField] private float _radius;

    protected override void Produce()
    {
        base.Produce();
        Mine newMine = Instantiate(_minePrefab, _effectsManager.Player.transform.position, Quaternion.identity);
        newMine.Init(_effectsManager.Player.Damage, _radius);
    }

}
