using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [Tooltip("Направление вращения")]
    [SerializeField] private Vector3 _rotationVector;

    void Update()
    {
        transform.Rotate(_rotationVector * Time.deltaTime);
    }
}
