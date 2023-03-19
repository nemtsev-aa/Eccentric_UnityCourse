using UnityEngine;

public class LootHeal : MonoBehaviour
{
    [Tooltip("���������� �������� � ��������")]
    [SerializeField] private int _lootValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out PlayerHealth playerHealth))
        {
            // ���� ����� �� ��������� ������ - ������� ��� �������
            if (playerHealth.Health < playerHealth.MaxHealth)
            {
                Take(playerHealth);
            }
        }
    }

    public void Take(PlayerHealth playerHealth)
    {
        playerHealth.AddHealth(_lootValue);
        Destroy(gameObject);
    }
}
