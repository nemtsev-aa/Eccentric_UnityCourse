using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Tooltip("����")]
    [SerializeField] private Transform _target;
    [Tooltip("�������� �������� ������ � ����")]
    [SerializeField] private float _lerpSpeed;

    public void SetTarget()
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
