using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("���� ����")]
    public int DamageValue = 1;
    [Tooltip("����� ����� ����")]
    [SerializeField] private float _lifeTime = 3f;
    [Tooltip("������ ���������")]
    public GameObject HitParticle;

    public event Action<GameObject, Bullet> HitRegistered;
    private int _ricochet = 1;

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
            else if (obstacle.TryGetComponent(out DownToTopMoving downToTopMoving))
            {
                downToTopMoving.enabled = true;
            }
            Hit(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
            Hit(collision.gameObject);
        else
            Ricochet();
    }

    public void Hit(GameObject collisionGameObject)
    {
        HitRegistered?.Invoke(collisionGameObject, this);
        // ������������� ��������� � ���������� ����
        Instantiate(HitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public virtual void Ricochet()
    {
        _ricochet ++;
    }

    public virtual int GetRicochetCount()
    {
        return _ricochet;
    }

}
