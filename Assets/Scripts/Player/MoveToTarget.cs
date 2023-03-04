using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [Tooltip("����")]
    [SerializeField] private Transform _target;
   
    void Update()
    {
        transform.position = _target.position;
    }
}
