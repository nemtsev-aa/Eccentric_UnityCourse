using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory : MonoBehaviour
{
    public abstract Unit CreateUnit1();
    public abstract Unit CreateUnit2();
    public abstract Unit CreateUnit3();

    public virtual void Setup(Transform spawnPoint, Transform ñollectionPoint) {

    }
}
