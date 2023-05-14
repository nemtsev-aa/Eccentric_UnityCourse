using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [Tooltip("Дистанция сбора")]
    [SerializeField] private float _distanceToCollect = 2f;
    [Tooltip("Физический слой для обработки сбора")]
    [SerializeField] private LayerMask _layerMask;
    [Tooltip("Менеджер опыта")]
    [SerializeField] private ExperienceManager _experienceManager;

    private void FixedUpdate()
    {
        // Массив коллайдеров с которыми взаимодействует сборщик
        Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceToCollect, _layerMask, QueryTriggerInteraction.Ignore);  
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Loot>() is Loot loot) loot.Collect(this); // Если объект взаимодействия - лут, активируем процедуру сборки
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
