using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    
    [Tooltip("Время существования")]
    [SerializeField] public float LifeTime;
    [Tooltip("Скорость перемещения")]
    [SerializeField] private float _speed;
    [Tooltip("Система частиц - выхлоп")]
    [SerializeField] private ParticleSystem _exhaust;
    [Tooltip("Система частиц - взрыв")]
    [SerializeField] private ParticleSystem _explosion;

    [SerializeField] private Dron_Controller _target;
    private bool inTarget;
    private float _time;
    private Rigidbody _rb;
    
    /// <summary>
    /// Нанесение урона
    /// </summary>
    public static event System.Action<Rocket> OnDetonation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _time = LifeTime;
    }
    public void Start()
    {
        _exhaust.Play();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _time -= Time.fixedDeltaTime;
        if (_time <= 0)
        {
            Detonation();
        }
        else
        {
            Move();
        }

    }
    public void SetTarget(Dron_Controller target)
    {
        _target = target;
    }
    /// <summary>
    /// Цель поражена
    /// </summary>
    private void Detonation()
    {
        _exhaust.Stop();
        ParticleSystem newExplosion = Instantiate(_explosion, transform.position, transform.rotation);
        newExplosion.Play();
        Destroy(gameObject);

        // Цель не поражена
        if (inTarget)
        {
            OnDetonation?.Invoke(this);
        } 
    }
   



    /// <summary>
    /// Движение к цели
    /// </summary>
    private void Move()
    {
        Vector3 toTarget = _target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(toTarget);
        _rb.velocity = toTarget.normalized * _speed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Dron_Controller>())
        {
            inTarget = true;
            Detonation();
        }
    }
}
