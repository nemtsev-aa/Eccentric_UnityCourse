using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    [Tooltip("����")]
    [SerializeField] private GameObject _target;
    [Tooltip("����� �������������")]
    [SerializeField] public float LifeTime;
    [Tooltip("�������� �����������")]
    [SerializeField] private float _speed;
    [Tooltip("������� ������ - ������")]
    [SerializeField] private ParticleSystem _exhaust;
    [Tooltip("������� ������ - �����")]
    [SerializeField] private ParticleSystem _explosion;


    private float _time;
    private Rigidbody _rb;
    /// <summary>
    /// ��������� �����
    /// </summary>
    public static event System.Action OnDealingDamage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _time = LifeTime;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        _time -= Time.fixedDeltaTime;
        if (_time <= 0)
        {
            RanOutFuel();
        }
        else
        {
            Move();
        }

    }
    /// <summary>
    /// ����������� ������� / ���� ����������
    /// </summary>
    private void RanOutFuel()
    {
        _exhaust.Stop();
        _explosion.Play();

        Invoke(nameof(Destroy), 1f);
    }
    /// <summary>
    /// �������� � ����
    /// </summary>
    private void Move()
    {
        _exhaust.Play();

        Vector3 relativePosition = _target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        _rb.rotation = rotation;
        _rb.velocity = transform.forward * _speed;
    }
    /// <summary>
    /// ����������� �������
    /// </summary>
    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dron"))
        {
            OnDealingDamage?.Invoke();
            RanOutFuel();
        }
    }
}
