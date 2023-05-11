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
    Bear,
    Spike,
    Shell,
    HermitKing
}

public class EnemyAnimal : Enemy
{
    [Header("Animal Settings")]
    // ������� ������ ��������� �������
    public bool IsActive = true;
    [Tooltip("��� ����������")]
    [SerializeField] public EnemyAnimalType EnemyAnimalType;
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

