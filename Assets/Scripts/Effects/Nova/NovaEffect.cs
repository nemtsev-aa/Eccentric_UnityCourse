using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(NovaEffect), menuName = "Effects/Continuous/" + nameof(NovaEffect))]
public class NovaEffect : ContinuousEffect
{
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;
    [SerializeField] private Nova _novaPrefab;
    [SerializeField] private LayerMask _layerMask;

    private Nova _currentNova;
    private float _damageBoost;

    public void Setup(float damageBoost)
    {
        _damageBoost = damageBoost;
    }

    protected override void FirstTimeCreated()
    {
        base.FirstTimeCreated();
        _currentNova = Instantiate(_novaPrefab, _player.transform);
        _currentNova.transform.localPosition = Vector3.zero;
    }

    protected override void Produce()
    {
        base.Produce();
        _currentNova.ShowEffect();
        Collider[] colliders = Physics.OverlapSphere(_currentNova.transform.position, _radius, _layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<EnemyHealth>() is EnemyHealth enemy)
                enemy.TakeDamage(_damage + _damageBoost);
        }
    }
}
