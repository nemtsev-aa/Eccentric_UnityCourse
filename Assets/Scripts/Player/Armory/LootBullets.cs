using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBullets : MonoBehaviour
{
    [Tooltip("������ �������� ������")]
    [SerializeField] private int _gunIndex;
    [Tooltip("���������� �������� � ��������")]
    [SerializeField] private int _lootValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out PlayerArmory playerArmory))
        {
            Take(playerArmory);
        }
    }

    public void Take(PlayerArmory playerArmory)
    {
        playerArmory.AddBullets(_gunIndex, _lootValue);
        Destroy(gameObject);
    }
}
