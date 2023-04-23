using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Star : ActiveItem
{
    [Header("Star")]
    [Tooltip("Радиус эффекта")]
    [SerializeField] private float _affectRadius = 1.5f;
    [Tooltip("Спрайт зоны эффекта")]
    [SerializeField] private GameObject _affectArea;
    [Tooltip("Визуальный эффект")]
    [SerializeField] private GameObject _affectPrefab;

    protected override void Start()
    {
        base.Start();
        _affectArea.SetActive(false);
    }

    private IEnumerator AffectProcess()
    {
        _affectArea.SetActive(true);
        _animator.enabled = true;
        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].attachedRigidbody)
            {
                colliders[i].attachedRigidbody.TryGetComponent(out ActiveItem item);
                if (item)
                    item.IncreaseLevel();
            }
        }

        Instantiate(_affectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        _affectArea.transform.localScale = Vector3.one * _affectRadius * 2f;
    }

    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(AffectProcess());
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, _affectRadius);
    }
#endif
}
