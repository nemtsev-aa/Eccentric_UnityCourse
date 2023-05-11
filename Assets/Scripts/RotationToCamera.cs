using UnityEngine;

public class RotationToCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    public void Init(Transform cameraTransform)
    {
        _cameraTransform = cameraTransform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_cameraTransform.position);
    }
}
