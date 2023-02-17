using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Transport>(out Transport transport))
        {
            transport.DestroyTransportStatus(true);
        }
    }
}
