using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Время жизни пули")]
    [SerializeField] private float _lifeTime = 3f;
    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
