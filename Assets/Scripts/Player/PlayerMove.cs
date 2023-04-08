using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Tooltip("���������� ���� ���������")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("������������ ���������")]
    [SerializeField] private Transform _transform;
    [Tooltip("�������� �����������")]
    [SerializeField] private float _moveSpeed;
    [Tooltip("������������ �������� �����������")]
    [SerializeField] private float _maxSpeed;
    [Tooltip("�������� ������")]
    [SerializeField] private float _jumpSpeed;
    [Tooltip("������������� ��������������� ��������")]
    [SerializeField] private float _friction;
    [Tooltip("���� �������� � �����")]
    [SerializeField] private float _torqueForce = 10f;
    
    [Tooltip("������ ���������� �� �����")]
    public bool Grounded;
    // ���������
    private DownToTopMoving _platform;
    // ���������� ������ � ������� ������ ������
    private int _jumpFrameCounter;

    private void Update()
    {
        float yScale = 0.8f;
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.S) || !Grounded )
            yScale = 0.65f;

        SetLocalScale(yScale);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Grounded)
                Jump();
        }
    }

    public void Jump()
    {
        _rigidbody.AddForce(0, _jumpSpeed, 0, ForceMode.VelocityChange);
        _jumpFrameCounter = 0;
    }

    private void SetLocalScale(float yScale)
    {
        _transform.localScale = Vector3.Lerp(_transform.localScale, new Vector3(1f, yScale, 1f), Time.deltaTime * 15f);
    }

    private void FixedUpdate()
    {
        float speedMultiplier = 1f;
        if (!Grounded)
        {
            speedMultiplier = 0.1f;
            // ������������ ����������� ������ � �����
            // ������-�����
            if ((_rigidbody.velocity.x > _maxSpeed && Input.GetAxis("Horizontal") > 0)
                || (_rigidbody.velocity.x < -_maxSpeed && Input.GetAxis("Horizontal") < 0))
                speedMultiplier = 0f;
        }

        // ���������� ������ ��� ������� �� �������/������ A,D
        _rigidbody.AddForce(Input.GetAxis("Horizontal") * _moveSpeed * speedMultiplier, 0, 0, ForceMode.VelocityChange);

        Vector3 relativeVelocity;
        if (_platform)
        {
            relativeVelocity = _rigidbody.velocity - _platform.Rigidbody.velocity;
            _rigidbody.AddForce(0, -relativeVelocity.y * _friction, 0, ForceMode.VelocityChange);
        }

        if (Grounded)
        {
            // ������������ ����������� ������ � ������� �������������
            _rigidbody.AddForce(-_rigidbody.velocity.x * _friction, 0, 0, ForceMode.VelocityChange);
            // ����������� ������ �� ������������� ��������� ����� ������
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 15f);
        }

        _jumpFrameCounter += 1;
        if (_jumpFrameCounter == 2)
        {
            _rigidbody.freezeRotation = false;
            _rigidbody.AddRelativeTorque(0f, 0f, _torqueForce, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Selectable selectable = collision.gameObject.GetComponent<Selectable>();
        if (selectable)
        {
            Grounded = true;
            selectable.Show(true);
        }

        DownToTopMoving platform = collision.gameObject.GetComponent<DownToTopMoving>();
        if (platform)
            _platform = platform;
    }

    private void OnCollisionExit(Collision collision)
    {
        Grounded = false;
        Selectable selectable = collision.gameObject.GetComponent<Selectable>();
        if (selectable)
            selectable.Show(false);
        
        if (_platform)
            _platform = null;
    }

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            float angle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);
            if (angle < 45f)
            {
                Grounded = true;
                _rigidbody.freezeRotation = true;
            }
        }

        DownToTopMoving moving = collision.gameObject.GetComponent<DownToTopMoving>();
        if (moving)
        {
            if (Input.GetKey(KeyCode.E) && moving.enabled == true)
                moving.ContinueMove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������� ������ ������ ��� ��������������� � �������
        if (other.TryGetComponent<Key>(out Key key))
           key.HitToKey();

        if (other.GetComponent<Exit>())
            GameProcessManager.Instance.GameWin();
    }
}
