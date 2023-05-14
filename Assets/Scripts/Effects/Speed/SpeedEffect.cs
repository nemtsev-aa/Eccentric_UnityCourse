using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SpeedEffect), menuName = "Effects/OneTime/" + nameof(SpeedEffect))]
public class SpeedEffect : OneTimeEffect
{
    [SerializeField] private float _speedToAdd = 2f;

    public override void Activate()
    {
        base.Activate();
        _effectsManager.Player.MoveSpeed += _speedToAdd;
    }
}
