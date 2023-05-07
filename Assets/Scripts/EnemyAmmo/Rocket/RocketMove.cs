using UnityEngine;

public class RocketMove : MonoBehaviour
{
    [Tooltip("Скорость перемещения")]
    public float MoveSpeed = 2f;
    [Tooltip("Скорость поворота")]
    public float RotationSpeed = 3f;
    // Цель
    private Transform _playerTransform;
    private bool _moveStatus;

    private void Start()
    {
        _playerTransform = FindObjectOfType<RigidbodyMove>().transform;
    }

    public void StartMove()
    {
        _moveStatus = true;
    }

    private void Update()
    {
        if (_moveStatus && _playerTransform != null)
        {
            transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0f);
            // Перемещаем ракету в направлении локальной оси Z
            transform.position += transform.forward * Time.deltaTime * MoveSpeed;
            // Определяем направление к цели (игроку)
            Vector3 toPlayer = _playerTransform.position - transform.position;
            // Определяем поворот
            Quaternion toPlayerRotation = Quaternion.LookRotation(toPlayer, Vector3.forward);
            // Корректируем поворот ракеты в плоскости ZX
            transform.rotation = Quaternion.Lerp(transform.rotation, toPlayerRotation, Time.deltaTime * RotationSpeed);
        }
    }

}
