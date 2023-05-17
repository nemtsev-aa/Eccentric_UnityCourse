using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knight : SelectableObject
{
    public NavMeshAgent NavMeshAgent;

    public override void WhenClickOnGround(Vector3 point) {
        base.WhenClickOnGround(point);

        NavMeshAgent.SetDestination(point);
    }
}
