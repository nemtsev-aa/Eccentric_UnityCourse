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
            if (collision.rigidbody.GetComponent<Bullet>())
            {
                // ��������� ������ ��������� ��� �������� ����������
                _enemyHealth.TakeDamage(1);
            }
        }

        if (DieOnAnyCollision)
        {
            _enemyHealth.Die();
        }
    }
}
