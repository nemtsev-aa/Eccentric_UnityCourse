using UnityEngine;

public class ShowTargetMark : MonoBehaviour
{
    [Tooltip("”казатель")]
    [SerializeField] private GameObject _mark;

    public void Show()
    {
        _mark.SetActive(true);
    }
    public void Hide()
    {
        _mark.SetActive(false);
    }
}
