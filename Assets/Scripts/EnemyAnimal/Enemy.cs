using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Здоровье противника")]
    [field: SerializeField] public EnemyHealth EnemyHealth { get; private set; }
    [Tooltip("Префаб - опыт")]
    [field: SerializeField] public GameObject _experienceLoot;
}
