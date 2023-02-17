using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingZone : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
