using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Здоровье противника")]
    [field: SerializeField] public EnemyHealth EnemyHealth { get; private set; }
    [Tooltip("Префаб - опыт")]
    [field: SerializeField] public GameObject _experienceLoot;
    
    [Tooltip("Период атаки")]
    [SerializeField] private float _attackPeriod = 1f;
    [Tooltip("Урон в секунду")]
    [SerializeField] private float _dps;

    private float _attackTimer;
    private PlayerHealth _playerHealth;
    private EnemyMove _enemyMove;

    private void Awake()
    {
        _enemyMove = gameObject.GetComponent<EnemyMove>();
    }
    
    public void Init(Transform playerTransform)
    {
        _enemyMove.Setup(playerTransform);
    }

    private void Update()
    {
        if (_playerHealth)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > _attackPeriod)
            {
                _playerHealth.TakeDamage(_dps * _attackPeriod);
                _attackTimer = 0;
            }    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() is PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() is PlayerHealth playerHealth)
        {
            _playerHealth = null;
        }
    }
}
