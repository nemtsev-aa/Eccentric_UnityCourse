using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DamageBoostEffect), menuName = "Effects/OneTime/" + nameof(DamageBoostEffect))]
public class DamageBoostEffect : OneTimeEffect
{
    [SerializeField] private float _damageToAdd = 2f;

    public override void Activate()
    {
        base.Activate();
        _effectsManager.Player.Damage += _damageToAdd;
    }
}
