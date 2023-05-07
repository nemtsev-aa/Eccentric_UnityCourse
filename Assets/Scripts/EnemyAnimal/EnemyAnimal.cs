#if UNITY_EDITOR
using UnityEditor;
#endif  

using System;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyAnimalType
{
    Hen,
    Rabbit,
    Pig,
    Squirrel,
    Bear
}

public class EnemyAnimal : Enemy
{
    [Header("Animal Settings")]
    // ������� ������ ��������� �������
    public bool IsActive = true;
    [Tooltip("��� ����������")]
    [SerializeField] public EnemyAnimalType EnemyAnimalType;
    [Tooltip("��������� ��� ���������")]
    [SerializeField] private float _distanceToActivate = 20f;
    [Tooltip("��������� �������� ����������")]
    [SerializeField] private Slider _healthView;
    [Tooltip("������ ������")]
    [SerializeField] private ParticleSystem _dieParticleEffect;
    [Tooltip("���� ��������� �����")]
    [SerializeField] private AudioClip _meHit;
    [Tooltip("���� ������")]
    [SerializeField] private AudioClip _meDie;

    [Tooltip("������� - ���� ���������")]
    public event Action<EnemyAnimal> EnemyKilled;
    //�������� �����
    private AudioSource _audioSource;
    // ������ ��������� �����
    private BlinkEffect _blinkEffect;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _blinkEffect = GetComponent<BlinkEffect>();
    }

    private void OnEnable()
    {
        EnemyHealth.HealthDecreased += ShowHealth;
        EnemyHealth.HealthIsOver += Die;
    }

    private void OnDisable()
    {
        EnemyHealth.HealthDecreased -= ShowHealth;
        EnemyHealth.HealthIsOver -= Die;
    }

    public void CheckDistance(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(transform.position, playerPosition);
        if (IsActive)
        {
            if (distance > _distanceToActivate + 2f)
                Deactivate();
        }
        else
        {
            if (distance < _distanceToActivate)
                Activate();
        }
    }

    public void Activate()
    {
        Debug.Log("Activate");
        gameObject.SetActive(true);
        IsActive = true;
    }

    public void Deactivate()
    {
        Debug.Log("Deactivate");
        gameObject.SetActive(false);
        IsActive = false;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.grey;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToActivate);
    }
    #endif

    private void Die(EnemyHealth enemyHealth)
    {
        // ������ ������ �����
        ParticleSystem dieParticle = Instantiate(_dieParticleEffect, enemyHealth.transform.position - Vector3.down * 1.5f, enemyHealth.transform.rotation);
        // �������� ����������� �� �������� �����
        EnemyKilled?.Invoke(this);
        // ����������� ���� ������
        PlayDieSound();
        // ������� ������ ����� �� �����
        Destroy(enemyHealth.gameObject);
        // ������ ������ ����� �� ����� ������ �����
        GameObject experienceLoot = Instantiate(_experienceLoot, transform.position, transform.rotation);
    }

    private void ShowHealth(int currentValue, int maxValue)
    {
        if (_blinkEffect != null)
            _blinkEffect.StartBlink();
        
        // ���������� �������� � ������� ��������
        float viewHealthValue = ((currentValue * 100) / maxValue) * 0.01f;
        _healthView.value = viewHealthValue;
        // ����������� ���� ��������� �����
        PlayHitSound();
    }

    private void PlayHitSound()
    {
        _audioSource.clip = _meHit;
        _audioSource.Play();
    }

    private void PlayDieSound()
    {
        _audioSource.clip = _meDie;
        _audioSource.Play();
    }
}

