using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : Unit
{
    private void Update() {
        if (_agent.velocity.magnitude > 0) {
            _animator.SetTrigger("Run");
            _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
        }
        else {
            _animator.SetTrigger("Idle");
        }
    }
}
