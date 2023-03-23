using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

enum EnemyType
{
    Hen,
    Rabbit,
    Pig,
    Squirrel,
    Bear
}

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("��� ����������")]
    [SerializeField] private EnemyType _enemyType;
    [Tooltip("������������ �������� ����������")]
    [SerializeField] private int _maxHealth = 1;
    [Tooltip("�������� ������ ����������")]
    [SerializeField] private AudioSource _meHit;
    [Tooltip("��������� �������� ����������")]
    [SerializeField] private Slider _healthView;
    [Tooltip("������ ������������")]
    [SerializeField] private bool _invulnerable;
    [Tooltip("������� - ��������� ����� �����������")]
    [SerializeField] private UnityEvent EventOnTakeDamage;
    // ������� �������� ����������
    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damageValue)
    {
        if (!_invulnerable)
        {
            EventOnTakeDamage.Invoke();
            _health -= damageValue;
            _meHit.Play();

            if (_healthView != null)
                ShowHealth();

            if (_health <= 0)
                Die();

            _invulnerable = true;

            // ����� ������������ ����������� � ����������� �� ����
            switch (_enemyType)
            {
                case EnemyType.Hen:
                    Invoke(nameof(StopInvulnerable), 1f);
                    break;
                case EnemyType.Rabbit:
                    Invoke(nameof(StopInvulnerable), 1.2f);
                    break;
                case EnemyType.Pig:
                    Invoke(nameof(StopInvulnerable), 2f);
                    break;
                case EnemyType.Squirrel:
                    Invoke(nameof(StopInvulnerable), 2.5f);
                    break;
                case EnemyType.Bear:
                    Invoke(nameof(StopInvulnerable), 3f);
                    break;
                default:
                    break;
            }
        }
    }

    private void StopInvulnerable()
    {
        _invulnerable = false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void ShowHealth()
    {
        float viewHealthValue = ((_health * 100) / _maxHealth) * 0.01f;
        _healthView.value = viewHealthValue;
    }
}
