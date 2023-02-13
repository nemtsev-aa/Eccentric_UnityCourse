using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Dron_Engine : MonoBehaviour, IEngine
{
    [Header("Engine Properties")]
    [SerializeField] private float _maxPower = 4f;
   

    [Header("Screw Properties")]
    [SerializeField] private Transform _screw;
    [SerializeField] private float _screwRotationSpeed = 80f;


    private Transform _droneTransform;
    void Start()
    {
        _droneTransform = FindObjectOfType<Dron_Mover>().gameObject.transform;
    }

    public void InitEngine()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateEngine(Rigidbody rb, Dron_Inputs input)
    {
        //Debug.Log("Running Engine: " + gameObject.name);
        Vector3 engineForce = Vector3.zero;
        engineForce = transform.up * ((rb.mass * Physics.gravity.magnitude) + (input.Throttle * _maxPower)) / 4f;
        rb.AddForce(engineForce, ForceMode.Force);
        
        HandleScrew();

        #region 3D Controls
        //Vector3 upVec = transform.up;
        //upVec.x = 0f;
        //upVec.z = 0f;
        //float diff = 1 - upVec.magnitude;
        //float finalDiff = Physics.gravity.magnitude * diff;
        //engineForce = transform.up * ((rb.mass * Physics.gravity.magnitude + finalDiff) + (input.Throttle * maxPower)) / 4f;
        //rb.AddForce(engineForce, ForceMode.Force);
        #endregion
    }

    private void HandleScrew()
    {
        if (!_screw)
        {
            return;
        }

        _screw.Rotate(Vector3.up, _screwRotationSpeed);
       
    }
}
