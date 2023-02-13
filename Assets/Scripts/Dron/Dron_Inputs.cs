using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Dron_Inputs : MonoBehaviour
{
    private Vector2 _cyclic;
    private float _pedals = 1f;
    private float _throttle;

    public Vector2 Cyclic { get => _cyclic; }
    public float Pedals { get => _pedals; }
    public float Throttle { get => _throttle; }

    private void OnCyclic(InputValue value)
    {
        _cyclic = value.Get<Vector2>();
    }

    private void OnPedals(InputValue value)
    {
        _pedals = value.Get<float>();
    }

    private void OnThrottle(InputValue value)
    {
        _throttle = value.Get<float>();
    }
}

