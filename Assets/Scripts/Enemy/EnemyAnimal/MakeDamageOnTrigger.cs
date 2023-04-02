using UnityEngine;

public class MakeDamageOnTrigger : MonoBehaviour
{
    [Tooltip("Количество урона")]
    [SerializeField] private int _damageValue = 1;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            if (other.attachedRigidbody.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(_damageValue);
            }
        }
    }
}
