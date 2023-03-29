using UnityEngine;

public class Key : MonoBehaviour
{
    [Tooltip("Объект активируемый ключём")]
    [SerializeField] private GameObject _target;
    [Tooltip("Камера")]
    [SerializeField] private CameraMove _cameraMove;

    public GameObject GetTarget()
    {
        Destroy(gameObject, 0.5f);
        GetComponent<AudioSource>().Play();
        _cameraMove.SetTarget(_target.transform);
        return _target;
    }


}
