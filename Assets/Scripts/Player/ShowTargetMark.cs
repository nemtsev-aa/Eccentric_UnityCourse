using UnityEngine;

public class ShowTargetMark : MonoBehaviour
{
    [Tooltip("Цель")]
    [SerializeField] private Transform _markTarget;

    public void SetTarget(Transform target)
    {
        _markTarget = target;
    }

    private void Update()
    {
        if (_markTarget)
        {
            Show();
            transform.position = _markTarget.position;
        }
        else
            Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);

    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
