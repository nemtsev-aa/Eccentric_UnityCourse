using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [Tooltip("Цель")]
    public Transform Target;
   
    void Update()
    {
        transform.position = Target.position;
    }
}
