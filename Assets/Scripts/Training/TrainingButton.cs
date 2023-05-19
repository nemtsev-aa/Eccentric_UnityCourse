using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingButton : MonoBehaviour
{
    public BarrackUnitsTrainer BarrackUnitsTrainer;
    public GameObject UnitPrefab;

    public void TryBuy() {
        int price = UnitPrefab.GetComponent<Unit>().Price;
        if (FindObjectOfType<Resources>().Money >= price) {
            FindObjectOfType<Resources>().Money -= price;
            BarrackUnitsTrainer.CreateUnit(UnitPrefab);
        } else {
            Debug.Log("Недостаточно денег!");
        }
    }
}
