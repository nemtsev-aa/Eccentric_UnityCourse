using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageOnCollision : MonoBehaviour
{
    [SerializeField] private int _damageValue = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject)
        {
            if (collision.gameObject.GetComponent<PlayerHealth>())
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(_damageValue);
            }
        }
    }
}
