using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Tooltip("����")]
    [SerializeField] private Transform _target;
    [Tooltip("�������� �������� ������ � ����")]
    [SerializeField] private float _lerpSpeed;

    public void SetTarget(Transform TargetTransform)
    {
        Time.timeScale = 0.1f;
        // ���� ������ - ��������
        _target = TargetTransform;
        Invoke(nameof(ResetTarget), 3f);
        Time.timeScale = 1f;
    }

    public void ResetTarget()
    {
        // ���� ������ - ��������
        _target = FindObjectOfType<PlayerMove>().transform;
    }

    void Update()
    {
        if (_target != null)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _lerpSpeed);
        }
    }

}
