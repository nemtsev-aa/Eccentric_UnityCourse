using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [Tooltip("Название")]
    public string Name;
    [Tooltip("Описание")]
    [TextArea(1, 3)]
    public string Description;
    [Tooltip("Спрайт")]
    public Sprite Sprite;
    [Tooltip("Текущий уровень")]
    public int Level = -1;

    protected EffectsManager _effectsManager;
    protected Player _player;
    protected EnemyManager _enemyManager;

    public virtual void Initialize(EffectsManager effectsManager, EnemyManager enemyManager, Player player)
    {
        _effectsManager = effectsManager;
        _enemyManager = enemyManager;
        _player = player;
    }

    public virtual void Activate()
    {
        Level++;
        if (Level == 0)
        {
            FirstTimeCreated();
        }
    }

    protected virtual void FirstTimeCreated()
    {
    }
}
