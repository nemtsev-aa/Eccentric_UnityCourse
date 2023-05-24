using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : Unit
{
    private void Update() {
        if (_navMeshAgent.velocity.magnitude > 0) {
            _animator.SetTrigger("Run");
            _animator.SetFloat("MoveSpeed", _navMeshAgent.velocity.magnitude);
        }
        else {
            _animator.SetTrigger("Idle");
        }
    }
}
