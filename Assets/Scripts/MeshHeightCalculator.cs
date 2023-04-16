using UnityEngine;

public class MeshHeightCalculator : MonoBehaviour
{
    public Mesh Mesh;
    public Material Material;
    public int MatIndex;
    public float FillValue;

    void Start()
    {
        Mesh = GetComponent<MeshFilter>().mesh;
        Material = gameObject.GetComponent<MeshRenderer>().materials[MatIndex];
    }

    public void FillingObject(float time)
    {
        FillValue = Mathf.Lerp(0, 1, time);
        Material.SetFloat("_Fill", FillValue);
    }
}
