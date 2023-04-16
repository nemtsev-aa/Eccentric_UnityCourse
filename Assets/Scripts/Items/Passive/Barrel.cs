using UnityEngine;

public class Barrel : PassiveItem
{
    [Tooltip("Визуальный эффект взрыва")]
    [SerializeField] private GameObject _barrelExplosion;

    public override void OnAffect()
    {
        base.OnAffect();
        Die();
    }

    [ContextMenu("Die")]
    void Die()
    {
        Instantiate(_barrelExplosion, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        Destroy(gameObject);
    }
}
