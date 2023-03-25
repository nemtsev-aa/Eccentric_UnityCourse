using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("����� ����� ����")]
    [SerializeField] private float _lifeTime = 3f;
    [Tooltip("������ ���������")]
    [SerializeField] private GameObject _hitParticle;

    public event Action<GameObject> HitRegistered;

    private void Start()
    {
        // ����������� ������� ��������� �� ������� - ���������
        HitRegistered += HitCounter.Instance.HitCounting;

        // ���������� ���� ����� ����������� �����, ���� ��� �� �������� ����
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody && other.attachedRigidbody.GetComponent<EnemyHealth>())
            Hit(other.gameObject);
        // ���������� ������ ������ ��� ��������� � ��� ����
        else if (other.TryGetComponent(out Key key))
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
            Hit(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
            Hit(collision.gameObject);
    }
    public void Hit(GameObject collisionGameObject)
    {
        HitRegistered?.Invoke(collisionGameObject);
        // ������������� ��������� � ���������� ����
        Instantiate(_hitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
