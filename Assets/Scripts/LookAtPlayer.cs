using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [Tooltip("Положение камеры")]
    [SerializeField] private Transform _cameraTransform;

    private void LateUpdate()
    {
        transform.LookAt(_cameraTransform);
    }
}
