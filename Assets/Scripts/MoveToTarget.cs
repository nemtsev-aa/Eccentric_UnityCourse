using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [Tooltip("����")]
    public Transform Target;
   
    void Update()
    {
        transform.position = Target.position;
    }
}
