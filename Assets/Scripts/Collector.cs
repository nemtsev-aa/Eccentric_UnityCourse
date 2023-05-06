using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [Tooltip("��������� �����")]
    [SerializeField] private float _distanceToCollect = 2f;
    [Tooltip("���������� ���� ��� ��������� �����")]
    [SerializeField] private LayerMask _layerMask;
    
    private void FixedUpdate()
    {
        // ������ ����������� � �������� ��������������� �������
        Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceToCollect, _layerMask, QueryTriggerInteraction.Ignore);  
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Loot>() is Loot loot) loot.Collect(this); // ���� ������ �������������� - ���, ���������� ��������� ������
        }
    }
}
