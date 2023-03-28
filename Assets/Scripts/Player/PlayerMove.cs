using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Tooltip("Физическое тело персонажа")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("Расположение персонажа")]
    [SerializeField] private Transform _transform;
    [Tooltip("Скорость перемещения")]
    [SerializeField] private float _moveSpeed;
    [Tooltip("Максимальная скорость перемещения")]
    [SerializeField] private float _maxSpeed;
    [Tooltip("Скорость прыжка")]
    [SerializeField] private float _jumpSpeed;
    [Tooltip("Сопротивление горизонтальному движению")]
    [SerializeField] private float _friction;
    [Tooltip("Статус нахождения на земле")]
    [SerializeField] private bool _grounded;
    [Tooltip("Источник луча PhysicRaycast")]
    [SerializeField] private Transform _rayStart;

    private void Update()
    {
        float yScale = 1f;
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.S) || !_grounded )
        {
            yScale = 0.7f;
        }
        SetLocalScale(yScale);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_grounded)
            {
                _rigidbody.AddForce(0, _jumpSpeed, 0, ForceMode.VelocityChange);
            }
        }

    }

    private void SetLocalScale(float yScale)
    {
        _transform.localScale = Vector3.Lerp(_transform.localScale, new Vector3(1f, yScale, 1f), Time.deltaTime * 15f);
    }

    private void FixedUpdate()
    {
        float speedMultiplier = 1f;
        if (!_grounded)
        {
            speedMultiplier = 0.1f;
            // Ограничиваем перемещение игрока в полёте
            // Вправо-влево
            if ((_rigidbody.velocity.x > _maxSpeed && Input.GetAxis("Horizontal") > 0)
                || (_rigidbody.velocity.x < -_maxSpeed && Input.GetAxis("Horizontal") < 0))
            {
                speedMultiplier = 0f;
            }
        }

        // Перемещаем игрока при нажатии на стрелки/кнопки A,D
        _rigidbody.AddForce(Input.GetAxis("Horizontal") * _moveSpeed * speedMultiplier, 0, 0, ForceMode.VelocityChange);
        
        if (_grounded)
        {
            // Ограничиваем перемещение игрока с помощью сопротивления
            _rigidbody.AddForce(-_rigidbody.velocity.x * _friction, 0, 0, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Selectable selectable = collision.gameObject.GetComponent<Selectable>();
        if (selectable)
        {
            _grounded = true;
            selectable.Show(true);
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            float angle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);
            if (angle < 45f)
            {
                _grounded = true;
            }         
        }

        DownToTopMoving moving = collision.gameObject.GetComponent<DownToTopMoving>();
        if (moving)
        {
            if (Input.GetKey(KeyCode.E))
            {
                moving.ContinueMove();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
        Selectable selectable = collision.gameObject.GetComponent<Selectable>();
        if (selectable)
        {
            selectable.Show(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Активируем эффект ключей при соприкосновении с игроком
        if (other.TryGetComponent<Key>(out Key key))
        {
            GameObject obstacle = key.GetTarget();

            if (obstacle.TryGetComponent(out LimitRotation limitRotation))
            {
                limitRotation.SetStatus(true);
            }
            else if (obstacle.TryGetComponent(out DownToTopMoving downToTopMoving))
            {
                downToTopMoving.enabled = true;
            }

            Destroy(key.gameObject);
        }

        if (other.GetComponent<Exit>())
        {
            GameProcessManager.Instance.GameWin();
        }
    }
}
