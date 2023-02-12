using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dron_RigidBody : MonoBehaviour
{
    [Header("RigidBody Properties")]
    [SerializeField] private float weightInLbs = 1f;

    const float lbsToKg = 0.454f;

    protected Rigidbody rb;
    protected float startDrag;
    protected float startAngularDrag;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.mass = weightInLbs * lbsToKg;
            startDrag = rb.drag;
            startAngularDrag = rb.angularDrag;
        }
    }
   
    void FixedUpdate()
    {
        if (!rb)
        {
            return;
        }

        HandlePhysics();
    }

    protected virtual void HandlePhysics() { }

}
