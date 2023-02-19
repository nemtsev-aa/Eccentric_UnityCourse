using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Tooltip("Высота расположения")]
    [SerializeField] private float _transformY;
    [Tooltip("Скорость вращения")]
    [SerializeField] private float _rotationSpeed;
    [Tooltip("Амплитуда вертикального перемещения")]
    [SerializeField] private float _upDownAmplitude;
    [Tooltip("Время жизни")]
    [SerializeField] private float _lifeTime;

    private float _time;
    /// <summary>
    /// Время жизни вышло
    /// </summary>
    public static event System.Action<Coin> OutLifeTime;

    void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _lifeTime)
        {
            OutLifeTime?.Invoke(this);
        }
        transform.position = new Vector3(transform.localPosition.x, _transformY + _upDownAmplitude*3 + Mathf.Sin(Time.time) * _upDownAmplitude, transform.localPosition.z);
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    } 

    public void SetLifeTime(float lifeTime)
    {
        _lifeTime = lifeTime;
    }
    public void SetTransformY(float transformY)
    {
       _transformY = transformY;
    }


}
