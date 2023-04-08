using TMPro;
using UnityEngine;

public class Automat : Gun
{
    [Header("Automat")]
    [Tooltip("Оставшееся количество пуль")]
    public int NumberOfBullets;
    [Tooltip("Запись - количество пуль")]
    public TextMeshProUGUI BulletsText;

    public PlayerArmory PlayerArmory;

    private void Start()
    {
        BulletsText.text = NumberOfBullets.ToString();
    }

    public override void Shot()
    {
        if (NumberOfBullets > 0)
        {
            base.Shot();
            NumberOfBullets -= 1;
            UpdateText(NumberOfBullets.ToString());
            if (NumberOfBullets == 0)
            {
                PlayerArmory.TakeGunByIndex(0);
            }
        }
        else
        {
            PlayerArmory.TakeGunByIndex(0);
        }
    }

    public override void Activate()
    {
        base.Activate();
        UpdateText(NumberOfBullets.ToString());
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    private void UpdateText(string text)
    {
        BulletsText.text = text;
    }

    public override void AddBullets(int numberOfBullets)
    {
        base.AddBullets(numberOfBullets);
        NumberOfBullets += numberOfBullets;
        UpdateText(NumberOfBullets.ToString());
        PlayerArmory.TakeGunByIndex(1);
    }
}
