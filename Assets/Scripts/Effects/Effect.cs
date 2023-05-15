using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [Tooltip("��������")]
    public string Name;
    [Tooltip("��������")]
    [TextArea(1, 3)]
    public string Description;
    [Tooltip("������")]
    public Sprite Sprite;
    [Tooltip("������� �������")]
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
