using UnityEngine;

public class AcornMove : MonoBehaviour
{
    [Tooltip("Сила броска")]
    [SerializeField] private Vector3 _velocity;
    [Tooltip("Максимальная угловая скорость")]
    [SerializeField] private float _maxRotationSpeed;

    private void Start()
    {
        // Инициализируем переменную для работы с твёрдым телом
        Rigidbody _acornRigidbody = GetComponent<Rigidbody>();
        // Прикладываем случайную силу во всех направлениях для закручивания ореха в полёте
        _acornRigidbody.AddRelativeForce(_velocity, ForceMode.VelocityChange);
        _acornRigidbody.angularVelocity = new Vector3(
            Random.Range(-_maxRotationSpeed, _maxRotationSpeed),
            Random.Range(-_maxRotationSpeed, _maxRotationSpeed),
            Random.Range(-_maxRotationSpeed, _maxRotationSpeed));
    }
}
