using UnityEngine;

public class TakeDamageOnCollision : MonoBehaviour
{
    [Tooltip("���� ���������� ����")]
    [SerializeField] private EnemyHealth _enemyHealth;
    [Tooltip("�������� ��� ����� ������������")]
    [SerializeField] private bool DieOnAnyCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            Bullet bullet = collision.rigidbody.GetComponent<Bullet>();
            RigidbodyMove rigidBodyMove = collision.rigidbody.GetComponent<RigidbodyMove>();
            if (bullet)
                // ��������� ������ ��������� ��� �������� ����������
                _enemyHealth.TakeDamage(bullet.DamageValue);
            else if (rigidBodyMove)
                // ��������� ������ �� ������������ � ������� ��� �������� ����������
                _enemyHealth.TakeDamage(1);
        }

        if (DieOnAnyCollision)
            _enemyHealth.Die();
    }
}
