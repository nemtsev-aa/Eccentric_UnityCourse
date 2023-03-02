using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [Tooltip("Объект прицела")]
    [SerializeField] private Transform _aim;
    [Tooltip("Камера игрока")]
    [SerializeField] private Camera _playerCamera;

    void Update()
    {
        // Луч из камеры игрока в позицию курсора мыши на экране
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        // Визуализация луча в сцене
        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.yellow);
        // Плоскость в которой проходит игровой процесс
        Plane plane = new Plane(-Vector3.forward, Vector3.zero);

        // Расстояние от камеры до плоскости
        float distance;
        // Измеряем расстояние от камеры до плоскости с помощью созданного луча
        plane.Raycast(ray, out distance);
        // Точка пересечения луча и плоскости
        Vector3 point = ray.GetPoint(distance);
        // Перемещаем прицел в току пересечения луча и плоскости
        _aim.position = point;


        Vector3 toAim = _aim.position - transform.position;
        transform.rotation = Quaternion.LookRotation(toAim);
    }
}
