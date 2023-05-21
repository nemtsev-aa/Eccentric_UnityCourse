using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingButton : MonoBehaviour
{
    public GameObject UnitPrefab;
    public UnitsTrainer _unitsTrainer;
    public Button _button;

    public void Init(UnitsTrainer unitsTrainer) {
        _unitsTrainer = unitsTrainer;
    }

    private void Start() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TryBuy);
    }

    public void TryBuy() {
        int price = UnitPrefab.GetComponent<Unit>().Price;
        Resources resources = FindObjectOfType<Resources>();
        if (resources.Money >= price) {
            FindObjectOfType<Resources>().Money -= price;
            _unitsTrainer.CreateUnit(UnitPrefab);
        } else {
            resources.NoGoldSoundEffect();
            Debug.Log("Недостаточно денег!");
        }
        resources.ShowRemainder();
    }
}
