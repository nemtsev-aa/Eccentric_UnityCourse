using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MagicMissleEffect), menuName = "Effects/Continuous/" + nameof(MagicMissleEffect))]
public class MagicMissleEffect : ContinuousEffect
{
    [Tooltip("Скрипт эффекта")]
    [SerializeField] private MagicMissle _magicMissle;
    [Tooltip("Скорость пули")]
    [SerializeField] private float _bulletSpeed;
    [Tooltip("Количество пуль")]
    [SerializeField] private int _bulletNumber;

    protected override void Produce()
    {
        base.Produce();
        _effectsManager.StartCoroutine(Effectprocess());
    }

    private IEnumerator Effectprocess()
    {
        EnemyAnimal[] nearestEnemies = _enemyManager.GetNearest(_player.transform.position, _bulletNumber); // Массив ближайших врагов
        if (nearestEnemies.Length > 0)
        {
            for (int i = 0; i < nearestEnemies.Length; i++)
            {
                Vector3 position = _player.transform.position;
                MagicMissle newBullet = Instantiate(_magicMissle, position, Quaternion.identity);
                newBullet.Setup(nearestEnemies[i], 20, _bulletSpeed);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
