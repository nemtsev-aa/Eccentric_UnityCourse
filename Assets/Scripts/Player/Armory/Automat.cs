using System;
using System.Collections;
using System.Collections.Generic;
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
    //public event Action BulletsAreOut;

    private void Start()
    {
        BulletsText.text = NumberOfBullets.ToString();
    }

    public override void Shot()
    {
        base.Shot();
        NumberOfBullets -= 1;
        UpdateText(NumberOfBullets.ToString());
        
        if (NumberOfBullets == 0)
        {
            //BulletsAreOut?.Invoke();
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
        UpdateText("9999");
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
