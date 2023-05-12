using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ShadowMisslesEffect), menuName = "Effects/Continuous/" + nameof(ShadowMisslesEffect))]
public class ShadowMisslesEffect : ContinuousEffect
{
    [Tooltip("Префаб пули")]
    [SerializeField] private ShadowMissle _shadowMissile;
    [Tooltip("Скорость пули")]
    [SerializeField] private float _bulletSpeed;
    [Tooltip("Количество пуль")]
    [SerializeField] private int _bulletNumber;
   
    private Transform _playerTransform;

    protected override void Produce()
    {
        base.Produce();
        _playerTransform = _player.transform;
        CreateBullets(_bulletNumber);
    }

    private void CreateBullets(int bulletNumber)
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            float angle = (360f / bulletNumber) * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * _playerTransform.forward;
            ShadowMissle newBullet = Instantiate(_shadowMissile, _playerTransform.position, Quaternion.identity);
            newBullet.Setup(direction * _bulletSpeed, 20, 2);
        }
    }
}
