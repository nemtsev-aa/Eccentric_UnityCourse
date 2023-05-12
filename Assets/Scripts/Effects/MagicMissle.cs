using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : MonoBehaviour
{
    private EnemyAnimal _targetEnemy; // ���� ��� �����
    private float _speed; // �������� 
    private float _damage; // ����

    public void Setup(EnemyAnimal targetEnemy, float damage, float speed)
    {
        _damage = damage;
        _targetEnemy = targetEnemy;
        _speed = speed;
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        if (_targetEnemy)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetEnemy.transform.position, _speed * Time.deltaTime); // ������������ � ����
            if (transform.position == _targetEnemy.transform.position)
            {
                AffectEnemy(); // �������� ����
                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject);
    }

    private void AffectEnemy()
    {
        _targetEnemy.GetComponent<EnemyHealth>().TakeDamage(_damage);
    }
}
