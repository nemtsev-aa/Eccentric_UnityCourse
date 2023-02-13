using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Dron_Inputs))]
public class Dron_Mover : Dron_RigidBody
{
    [Header("Control Properties")]
    [SerializeField] private float _minMaxPitch = 30f;
    [SerializeField] private float _minMaxRoll = 30f;
    [SerializeField] private float _yawPower = 4f;
    [SerializeField] private float _lerpSpeed = 10f;

    private Dron_Inputs _input;
    private List<IEngine> _engines = new List<IEngine>();

    private float _finalPitch;
    private float _finalRoll;
    private float _finalYaw;
    private float _yaw;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<Dron_Inputs>();
        _engines = GetComponentsInChildren<IEngine>().ToList<IEngine>();
    }

    protected override void HandlePhysics()
    {
        HandleEngines();
        HandleControls();

    }

    protected virtual void HandleEngines()
    {
        //rb.AddForce(Vector3.up * (rb.mass * Physics.gravity.magnitude));
        foreach (IEngine engine in _engines)
        {
            engine.UpdateEngine(_rb, _input);
        }
    }

    protected virtual void HandleControls()
    {
        float pitch = _input.Cyclic.y * _minMaxPitch;
        float roll = -_input.Cyclic.x * _minMaxRoll;
        _yaw = _input.Pedals;

        _finalRoll = Mathf.Lerp(_finalRoll, roll, Time.deltaTime * _lerpSpeed);
        _finalYaw = Mathf.Lerp(_finalYaw, _yaw, Time.deltaTime * _lerpSpeed * 1.5f);

        Quaternion rot = Quaternion.Euler(_finalRoll, _finalYaw * 90f, 0f);
        _rb.MoveRotation(rot);

        #region 3D Controls
        //yaw = input.Pedals * yawPower;
        //finalPitch = Mathf.Lerp(finalPitch, pitch, Time.deltaTime * lerpSpeed);
        //finalRoll = Mathf.Lerp(finalRoll, roll, Time.deltaTime * lerpSpeed);
        //finalYaw = Mathf.Lerp(finalYaw, yaw, Time.deltaTime * lerpSpeed);
        //Quaternion rot = Quaternion.Euler(finalPitch, finalYaw, finalRoll);
        //rb.MoveRotation(rot);
        #endregion
    }
}
