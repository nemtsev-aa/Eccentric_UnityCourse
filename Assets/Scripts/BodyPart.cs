using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] public bool Status;
    [SerializeField] public string NameRu;
    [SerializeField] public float RatioScale;
    [SerializeField] public BodyPartsName BodyPartName;
    [SerializeField] public Material Material;
    [SerializeField] private Outline _outline;

    public void SetStatus(bool value)
    {
        Status = value;
        _outline.enabled = value;
    }

    public void SetScale(float value)
    {
        RatioScale = value;
        Vector3 newScale = Vector3.one * RatioScale;
        transform.localScale = newScale;
    }
   
    public void SetColor(Material value)
    {
        Material.color = value.color;
    }
}

