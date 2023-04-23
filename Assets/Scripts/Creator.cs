using System.Collections;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [Tooltip("Положение трубы")]
    [SerializeField] private Transform _tube;
    [Tooltip("Положение генератора шаров")]
    [SerializeField] private Transform _spawner;
    [Tooltip("Префаб создаваемых объектов")]
    [SerializeField] private ActiveItem _ballPrefab;
    [Tooltip("Положение начала луча")]
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private LayerMask _layerMask;
    
    // Шар в трубе
    private ActiveItem _itemInTube;
    // Шар в манипуляторе
    private ActiveItem _itemInSpawner;

    void Start()
    {
        CreateItemInTube();
        StartCoroutine(MoveToSpawner());
    }

    void CreateItemInTube()
    {
        int itemLevel = Random.Range(0, 5);
        _itemInTube = Instantiate(_ballPrefab, _tube.position, Quaternion.identity);
        //_itemInTube.SetLevel(itemLevel);
        _itemInTube.SetupToTube();
    }

    private IEnumerator MoveToSpawner()
    {
        _itemInTube.transform.parent = _spawner;
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.45f)
        {
            _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
            yield return null;
        }
        _itemInTube.transform.localPosition = Vector3.zero;
        _itemInSpawner = _itemInTube;
        _rayTransform.gameObject.SetActive(true);
        _itemInSpawner.Projection.Show();
        _itemInTube = null;
        CreateItemInTube();
    }

    private void LateUpdate()
    {
        if (_itemInSpawner)
        {
            Ray ray = new Ray(_spawner.position, Vector3.down);
            RaycastHit hit;
            if (Physics.SphereCast(ray, _itemInSpawner.Radius, out hit, 100f, _layerMask, QueryTriggerInteraction.Ignore))
            {
                _rayTransform.localScale = new Vector3(_itemInSpawner.Radius * 2, hit.distance, 1f);
                _itemInSpawner.Projection.SetPosition(_spawner.position + Vector3.down * hit.distance);
            }

            if (Input.GetMouseButtonUp(0))
                Drop();
        }
    }

    void Drop()
    {
        _itemInSpawner.Drop();
        _itemInSpawner.Projection.Hide();
        _itemInSpawner = null;
        _rayTransform.gameObject.SetActive(false);
        if (_itemInTube)
            StartCoroutine(MoveToSpawner());
    }
}
