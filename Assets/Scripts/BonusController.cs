using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    [Header("Скорость вращения")]
    public float RotationSpeed = 200f;
    //Событие - сбор приза
    public static event System.Action<BonusController> OnBonusPic;
    // Update is called once per frame
    void Update()
    {
        Rotation();
        
    }

    private void Rotation()
    {
        transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMover>())
        {
            OnBonusPic?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
