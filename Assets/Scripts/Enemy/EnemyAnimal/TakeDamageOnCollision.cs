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
            if (bullet)
                // ��������� ������ ��������� ��� �������� ����������
                _enemyHealth.TakeDamage(bullet.DamageValue);
        }

        if (DieOnAnyCollision)
            _enemyHealth.Die();
    }
}
