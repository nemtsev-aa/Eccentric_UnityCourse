using UnityEngine;

public class Hook : MonoBehaviour
{
    [Tooltip("���������� ���� �����")]
    public Rigidbody Rigidbody;
    [Tooltip("���������� ������������� �����")]
    public GameObject _hookObject;
    [Tooltip("��������� �����")]
    public Collider Collider;
    [Tooltip("��������� ������")]
    public Collider PlayerColloder;
    [Tooltip("����������")]
    public RopeGun RopeGun;

    // ����������� ���������� 
    private FixedJoint _fixedJoint;
    private void Start()
    {
        Physics.IgnoreCollision(Collider, PlayerColloder);
    }

    private void Update()
    {
        if (_fixedJoint)
        {
            if (_fixedJoint.connectedBody == null)
                RopeGun.DestroySpring();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_fixedJoint == null)
        {
            // ������ ��������� FixedJoint
            _fixedJoint = gameObject.AddComponent<FixedJoint>();

            if (collision.rigidbody)
            {
                Debug.Log("collision.rigidbody" + collision.rigidbody.gameObject.name);
                // ����������� FixedJoint � ������� ����, � ������� ���������� ����
                _fixedJoint.connectedBody = collision.rigidbody;
                // ������ ������
                RopeGun.CreateSpring();

                if (collision.rigidbody.GetComponent<EnemyAmmo>() || collision.rigidbody.GetComponent<Bullet>())
                    StopFix();
            } 
        }
    }

    public void StopFix()
    {
        // ���������� ����������� ����������
        if (_fixedJoint)
            Destroy(_fixedJoint);
    }
}
