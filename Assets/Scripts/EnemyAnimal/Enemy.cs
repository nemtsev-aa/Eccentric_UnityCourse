using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("�������� ����������")]
    [field: SerializeField] public EnemyHealth EnemyHealth { get; private set; }
    [Tooltip("������ - ����")]
    [field: SerializeField] public GameObject _experienceLoot;
}