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
            if (other.attachedRigidbody.GetComponent<Bullet>())
            {
                // ��������� ������ ��������� ��� �������� ����������
                _enemyHealth.TakeDamage(1);
                // ���������� ����, �������� � ����������
                Destroy(other.gameObject);
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
