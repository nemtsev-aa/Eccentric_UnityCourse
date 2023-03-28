using UnityEngine;

public class Key : MonoBehaviour
{
    [Tooltip("Объект активируемый ключём")]
    [SerializeField] private GameObject _target;

    public GameObject GetTarget()
    {
        Destroy(gameObject, 0.5f);
        GetComponent<AudioSource>().Play();
        return _target;
    }
}
