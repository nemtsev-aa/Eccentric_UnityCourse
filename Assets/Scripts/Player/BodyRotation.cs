using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    //[Tooltip("Цель")]
    //[SerializeField] private Transform _aim;
    //[Tooltip("Угол поворота тела вокруг оси Y")]
    //[SerializeField] private float bodyYRotation = 30f;
    //[Tooltip("Скорость поворота к цели")]
    //[SerializeField] private float _lerpSpeed;

    //void Update()
    //{
    //    // Тернарный оператор
    //    Quaternion yQuaternion = Quaternion.AngleAxis(_aim.position.x > transform.position.x ? -bodyYRotation : bodyYRotation, Vector3.up);
    //    // Поворот относительно Y
    //    transform.localRotation = Quaternion.RotateTowards(transform.rotation, yQuaternion, Time.deltaTime * _lerpSpeed);
    //}

    [Tooltip("Цель")]
    [SerializeField] private Transform _target;
    [Tooltip("Предел поворота тела влево")]
    [SerializeField] private Vector3 _bodyRight = new Vector3(0, -30, 0);
    [Tooltip("Предел поворота тела вправо")]
    [SerializeField] private Vector3 _bodyLeft = new Vector3(0, 30, 0);

    void Update()
    {
        // Смещение курсора по оси Y, относительно головы
        float yAimOffset = transform.position.x - _target.position.x;
        float interpolant = Mathf.InverseLerp(-3f, 3f, yAimOffset);
        // Поворот головы в отностиельных координатах
        transform.localRotation = Quaternion.Lerp(Quaternion.Euler(_bodyRight), Quaternion.Euler(_bodyLeft), interpolant);
    }
}
