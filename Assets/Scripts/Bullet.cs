using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("����� ����� ����")]
    [SerializeField] private float _lifeTime = 3f;
    [Tooltip("������ ���������")]
    [SerializeField] private GameObject _hitParticle;

    public event Action HitRegistered;

    private void Start()
    {
        // ����������� ������� ��������� �� ������� - ���������
        HitRegistered += HitCounter.Instance.HitCounting;

        // ���������� ���� ����� ����������� �����, ���� ��� �� �������� ����
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������� ������ ������ ��� ��������� � ��� ����
        if (other.TryGetComponent<Key>(out Key key))
        {
            GameObject obstacle = key.GetTarget();

            if (obstacle.TryGetComponent(out LimitRotation limitRotation))
            {
                limitRotation.SetStatus(true);
            }
            else if (obstacle.TryGetComponent(out LimitMoving limitMoving))
            {
                limitMoving.SetStatus(true);
            }
        }
        Hit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            Hit();
        } 
    }
    public void Hit()
    {
        HitRegistered?.Invoke();
        // ������������� ��������� � ���������� ����
        Instantiate(_hitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
