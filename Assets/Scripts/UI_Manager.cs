using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bodyPartName;
    [SerializeField] private Slider _bodyPartScale;
    [SerializeField] private ScaleValue _scaleValueLabel;
    [SerializeField] private Transform _colorContainer;

    private void OnEnable()
    {
        BodyPartsSwitching.BodyPartSwitching += SetBodyName;
    }

    private void OnDisable()
    {
        BodyPartsSwitching.BodyPartSwitching -= SetBodyName;
    }

    private void SetBodyName(BodyPart obj)
    {
        _bodyPartName.text = obj.NameRu;
        _bodyPartScale.value = obj.RatioScale;
        _scaleValueLabel.SetValue(_bodyPartScale.value);
        if (GetColorToggle(obj.Material).isOn != true)
        {
            GetColorToggle(obj.Material).isOn = true;
        }
        
    }
    private Toggle GetColorToggle(Material material)
    {
        foreach (Transform iToggle in _colorContainer)
        {
            Material iMaterial = iToggle.GetComponent<ColorMaterial>().Material;

            if (iMaterial.color == material.color)
            {
                Toggle activeToggle = iToggle.GetComponent<Toggle>();
                return activeToggle;
            }
        }
        return null;
    }
}

