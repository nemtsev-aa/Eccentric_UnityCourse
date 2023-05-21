using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum KnightType {
    Standart,
    Light,
    Heavy
}
public class Knight : Unit
{
    [Space(10)]
    public KnightType KnightType;
}
