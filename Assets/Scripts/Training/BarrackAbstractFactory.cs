using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BarrackAbstractFactory : MonoBehaviour
{
    public abstract Unit CreateStandartKnight();
    public abstract Unit CreateLightKnight();
    public abstract Unit CreateHeavyKnight();

    public virtual void Setup(Transform spawnPoint, Transform ñollectionPoint) {

    }
}
