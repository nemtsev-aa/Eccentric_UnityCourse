using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [Tooltip("��������� �����")]
    [SerializeField] private float _distanceToCollect = 2f;
    [Tooltip("���������� ���� ��� ��������� �����")]
    [SerializeField] private LayerMask _layerMask;
    [Tooltip("�������� �����")]
    [SerializeField] private ExperienceManager _experienceManager;

    private void FixedUpdate()
    {
        // ������ ����������� � �������� ��������������� �������
        Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceToCollect, _layerMask, QueryTriggerInteraction.Ignore);  
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Loot>() is Loot loot) loot.Collect(this); // ���� ������ �������������� - ���, ���������� ��������� ������
        }
    }

    public void TakeExperince(int value)
    {
        _experienceManager.AddExperience(value);
    }

    public void CollectionDistanceUpdate(float value)
    {
        _distanceToCollect = value;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToCollect);
    }
#endif
}
