using System.Collections;
using UnityEngine;


public class Dinamit : ActiveItem
{
    [Header("Dinamit")]
    [Tooltip("Радиус эффекта")]
    [SerializeField] private float _effectRadius;
    [Tooltip("Значение силы")]
    [SerializeField] private float _forceValue;
    [Tooltip("Спрайт зоны взрыва")]
    [SerializeField] private GameObject _affectArea;
    [Tooltip("Эффект взрыва")]
    [SerializeField] private GameObject _affectPrefab;

    protected override void Start()
    {
        base.Start();
        _affectArea.SetActive(false);
    }

    [ContextMenu("Explode")]
    public void Explode()
    {
        StartCoroutine(EffectProcess());
    }

    private IEnumerator EffectProcess()
    {
        _affectArea.SetActive(true);
        _animator.enabled = true;
        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _effectRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if (rigidbody)
            {
                Vector3 fromTo = (rigidbody.transform.position - transform.position).normalized;
                rigidbody.AddForce(fromTo * _forceValue + Vector3.up * _forceValue * 0.5f);

                PassiveItem passiveItem = rigidbody.GetComponent<PassiveItem>();
                if (passiveItem)
                    passiveItem.OnAffect();
            }
        }
        Instantiate(_affectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        _affectArea.transform.localScale = Vector3.one * _effectRadius * 2f;
    }

    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(EffectProcess());
    }
}
