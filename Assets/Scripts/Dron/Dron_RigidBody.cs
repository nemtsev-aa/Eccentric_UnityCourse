using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dron_RigidBody : MonoBehaviour
{
    [Header("RigidBody Properties")]
    [SerializeField] private float _weightInLbs = 1f;

    const float lbsToKg = 0.454f;

    protected Rigidbody _rb;
    protected float _startDrag;
    protected float _startAngularDrag;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb)
        {
            _rb.mass = _weightInLbs * lbsToKg;
            _startDrag = _rb.drag;
            _startAngularDrag = _rb.angularDrag;
        }
    }
   
    void FixedUpdate()
    {
        if (!_rb)
        {
            return;
        }

        HandlePhysics();
    }

    protected virtual void HandlePhysics() { }

}
