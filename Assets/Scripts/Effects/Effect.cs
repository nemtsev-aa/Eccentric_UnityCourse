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
    public int Level = 0;

}
