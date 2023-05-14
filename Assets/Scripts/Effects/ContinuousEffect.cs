using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousEffect : Effect
{
    [Tooltip("Задержка при повторении")]
    [SerializeField] private float _colldown;
    private float _timer;

    public void ProcessFrame(float frameTime) // Метод реализующий периодическое действие эффекта
    {
        _timer += frameTime;
        if (_timer > _effectsManager.Player.Colldown)
        {
            Produce();
            _timer = 0;
        }
    }

    protected virtual void Produce() // Метод для реализации уникальной механики каждого эффекта
    {

    }
}
