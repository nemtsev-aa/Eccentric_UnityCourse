using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    [Tooltip("Цель")]
    [SerializeField] private Transform _target;
    //[Tooltip("Скорость поворота к цели")]
    //[SerializeField] private float _lerpSpeed = 10f;
    [Tooltip("Предел поворота головы вверх")]
    [SerializeField] private Vector3 _headUp = new Vector3(15, 0, 0);
    [Tooltip("Предел поворота головы вниз")]
    [SerializeField] private Vector3 _headDown = new Vector3(-15, 0, 0);

    void Update()
    {
        // Смещение курсора по оси Y, относительно головы
        float yAimOffset =  transform.position.y - _target.position.y;
        float interpolant = Mathf.InverseLerp(-5f, 5f, yAimOffset);
        // Поворот головы в отностиельных координатах
        transform.localRotation = Quaternion.Lerp(Quaternion.Euler(_headUp), Quaternion.Euler(_headDown), interpolant);
    }
}
