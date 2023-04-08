using TMPro;
using UnityEngine;

public class Pistol : Gun
{
    [Tooltip("Запись - количество пуль")]
    public TextMeshProUGUI BulletsText;

    public override void Activate()
    {
        base.Activate();
        UpdateText("Infinity");
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
    private void UpdateText(string text)
    {
        BulletsText.text = text;
    }
}
