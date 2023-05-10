using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [Tooltip("��������� ������")]
    [SerializeField] private Transform _cameraTransform;

    private void LateUpdate()
    {
        transform.LookAt(_cameraTransform);
    }
}
