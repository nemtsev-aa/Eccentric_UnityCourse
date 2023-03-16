using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimationManager : MonoBehaviour
{
    [SerializeField] private float _attackPeriod = 7f;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _attackPeriod)
        {
            _timer = 0;
            _animator.SetTrigger("Attack");
        }
    }
}
