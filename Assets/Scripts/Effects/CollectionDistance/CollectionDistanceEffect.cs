using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CollectionDistanceEffect), menuName = "Effects/OneTime/" + nameof(CollectionDistanceEffect))]
public class CollectionDistanceEffect : OneTimeEffect
{
    [SerializeField] private float _distanceToAdd = 2f;

    public override void Activate()
    {
        base.Activate();
        _effectsManager.Player.CollectionDistance += _distanceToAdd;
    }
}
