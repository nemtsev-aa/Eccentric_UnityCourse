using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    [Tooltip("����")]
    [SerializeField] private Transform _target;
    [Tooltip("���� �������� ���� ������ ��� Y")]
    [SerializeField] private float bodyYRotation = 30f;
    [Tooltip("�������� �������� � ����")]
    [SerializeField] private float _lerpSpeed;

    void Update()
    {
        // ��������� ��������
        Quaternion yQuaternion = Quaternion.AngleAxis(_target.position.x > transform.position.x ? -bodyYRotation : bodyYRotation, Vector3.up);
        // ������� ������������ Y
        transform.rotation = Quaternion.Lerp(transform.rotation, yQuaternion, Time.deltaTime * _lerpSpeed);

        // ��������� ������
        //if (_target.position.x > 0)
        //{
        //    bodyYRotation = -bodyYRotation;
        //}
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(bodyYRotation, Vector3.up), Time.deltaTime * _lerpSpeed);
    }
}
