using UnityEngine;

public class Key : MonoBehaviour
{
    [Tooltip("������ ������������ ������")]
    [SerializeField] private GameObject _target;
    [Tooltip("������")]
    [SerializeField] private CameraMove _cameraMove;

    public void HitToKey()
    { 
        GetComponent<AudioSource>().Play();
        _cameraMove.SetTarget(_target.transform);

        ActivateTarget();
    }

    private void ActivateTarget()
    {
        Destroy(gameObject);

        if (_target.TryGetComponent(out LimitRotation limitRotation))
        {
            limitRotation.SetStatus(true);
        }
        else if (_target.TryGetComponent(out DownToTopMoving downToTopMoving))
        {
            downToTopMoving.enabled = true;
        }
    }
}
