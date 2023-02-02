using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Header("Скорость перемещения")]
    [Range(3, 7)]
    public float Speed = 3f;
    [Header("Ускорение")]
    [Range(20, 25)]
    public float Boost = 25f;
    [Header("Скорость вращения")]
    [Range(-600, 600)]
    public float RotationSpeed = -600f;
    [Header("Масштаб сжатия")]
    [Range(0.7f, 0.9f)]
    public float LocalScaleY = 0.7f;

    void Update()
    {
        Move();
        Rotation();
        Compression();
    }

    /// Перемещение игрока
    private void Move()
    {
        //Смещение по горизонтильной оси
        float h = Input.GetAxis("Horizontal");
        //Смещение по вертикальной оси
        float v = Input.GetAxis("Vertical");
        //Общее смещение
        Vector3 offset;

        //Управляем скоростью персонажа
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Движение с ускорением
            offset = new Vector3(h, 0, v) * Boost * Time.deltaTime;
        }
        else
        {
            //Нормальное движение
            offset = new Vector3(h, 0, v) * Speed * Time.deltaTime;
        }

        transform.Translate(offset);
    }
    /// Поворот игрока
    private void Rotation()
    {
        //Смещение мыши по горизонтальной оси
        float yRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, yRotation * RotationSpeed * Time.deltaTime, 0);
    }
    /// Изменение размера игрока
    private void Compression()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            //Изменённый размер
            transform.localScale = new Vector3(1f, LocalScaleY, 1f);
        }
        else
        {
            //Нормальный размер
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }     
}
