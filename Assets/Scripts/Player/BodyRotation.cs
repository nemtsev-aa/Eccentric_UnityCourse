using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    //[Tooltip("����")]
    //[SerializeField] private Transform _aim;
    //[Tooltip("���� �������� ���� ������ ��� Y")]
    //[SerializeField] private float bodyYRotation = 30f;
    //[Tooltip("�������� �������� � ����")]
    //[SerializeField] private float _lerpSpeed;

    //void Update()
    //{
    //    // ��������� ��������
    //    Quaternion yQuaternion = Quaternion.AngleAxis(_aim.position.x > transform.position.x ? -bodyYRotation : bodyYRotation, Vector3.up);
    //    // ������� ������������ Y
    //    transform.localRotation = Quaternion.RotateTowards(transform.rotation, yQuaternion, Time.deltaTime * _lerpSpeed);
    //}

    [Tooltip("����")]
    [SerializeField] private Transform _target;
    [Tooltip("������ �������� ���� �����")]
    [SerializeField] private Vector3 _bodyRight = new Vector3(0, -30, 0);
    [Tooltip("������ �������� ���� ������")]
    [SerializeField] private Vector3 _bodyLeft = new Vector3(0, 30, 0);

    void Update()
    {
        // �������� ������� �� ��� Y, ������������ ������
        float yAimOffset = transform.position.x - _target.position.x;
        float interpolant = Mathf.InverseLerp(-3f, 3f, yAimOffset);
        // ������� ������ � ������������� �����������
        transform.localRotation = Quaternion.Lerp(Quaternion.Euler(_bodyRight), Quaternion.Euler(_bodyLeft), interpolant);
    }
}
