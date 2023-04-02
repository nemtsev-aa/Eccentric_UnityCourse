using UnityEngine;

public class MakeDamageOnCollision : MonoBehaviour
{
    [Tooltip("Количество урона")]
    [SerializeField] private int _damageValue = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject)
        {
            if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(_damageValue);
            }
        }
    }
}
