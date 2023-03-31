using UnityEngine;

public class TakeDamageOnTrigger : MonoBehaviour
{
    [Tooltip("���� ���������� ����")]
    [SerializeField] private EnemyHealth _enemyHealth;
    [Tooltip("�������� ��� ����� ������������")]
    [SerializeField] private bool DieOnAnyCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            Bullet bullet = other.attachedRigidbody.GetComponent<Bullet>();
            if (bullet)
            {
                // ��������� ������ ��������� ��� �������� ����������
                _enemyHealth.TakeDamage(bullet.DamageValue);
                // ���������� ����, �������� � ����������
                Destroy(other.gameObject);
            }
            PlayerHealth playerHealth = other.attachedRigidbody.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                _enemyHealth.Die();
            }
        }

        if (DieOnAnyCollision)
        {
            if (!other.isTrigger)
            {
                _enemyHealth.Die();
            } 
        }
    }
}
