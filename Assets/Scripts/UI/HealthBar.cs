using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Tooltip("Полоса здоровья")]
    public Transform ScaleTransform;
    [Tooltip("Объект - цель")]
    public Transform Target;
    private Transform _cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Target.position + Vector3.up * 1.5f;
        transform.rotation = _cameraTransform.rotation;
    }

    public void Setup(Transform target) {
        Target = target;
    }

    public void SetHealth(int health, int maxHealth) {
        float xScale = (float)health / maxHealth;
        xScale = Mathf.Clamp01(xScale);
        ScaleTransform.localScale = new Vector3(xScale, 1f, 1f);
    }
}
