using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [Tooltip("����������� ��������")]
    [SerializeField] private Vector3 _rotationVector;

    void Update()
    {
        transform.Rotate(_rotationVector * Time.deltaTime);
    }
}
