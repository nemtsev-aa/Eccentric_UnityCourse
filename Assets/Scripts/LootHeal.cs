using UnityEngine;

public class LootHeal : MonoBehaviour
{
    [Tooltip("Количество здоровья в предмете")]
    [SerializeField] private int _lootValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out PlayerHealth playerHealth))
        {
            // Если игрок не полностью здоров - передаём ему лечение
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
