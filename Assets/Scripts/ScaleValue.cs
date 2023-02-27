using UnityEngine;
using TMPro;


public class ScaleValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetValue(float value)
    {
        _text.text = value.ToString("0.00");
    }

}
