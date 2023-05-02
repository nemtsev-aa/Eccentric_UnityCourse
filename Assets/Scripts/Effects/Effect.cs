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
    public int Level = 0;

}
