#if UNITY_EDITOR
using UnityEditor;
#endif    

using UnityEngine;

public class ActivateByDistance : MonoBehaviour
{
    [Tooltip("Дистанция для активации")]
    [SerializeField] private float _distanceToActivate = 20f;
    // Текущий статус активации объекта
    public bool IsActive = true;
    
    public void CheckDistance(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(transform.position, playerPosition);
        if (IsActive)
        {
            if (distance > _distanceToActivate + 2f)
                Deactivate();
        }
        else
        {
            if (distance < _distanceToActivate)
                Activate();
        } 
    }

    public void Activate()
    {
        Debug.Log("Activate");
        gameObject.SetActive(true);
        IsActive = true;
    }

    public void Deactivate()
    {
        Debug.Log("Deactivate");
        gameObject.SetActive(false);
        IsActive = false;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.grey;
        Handles.DrawWireDisc(transform.position, Vector3.forward, _distanceToActivate);
    }
    #endif
}
