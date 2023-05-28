using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingButton : MonoBehaviour
{
    public Unit Unit;
    public UnitsTrainer _unitsTrainer;
    public Button _button;
    private Resources _resources;

    public void Init(UnitsTrainer unitsTrainer) {
        _unitsTrainer = unitsTrainer;
    }

    private void Start() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TryBuy);
        _resources = FindObjectOfType<Resources>();
    }

    public void TryBuy() {
        int price = Unit.Price;
       
        if (_resources.Money >= price) {
            _resources.Money -= price;
            _unitsTrainer.CreateUnit(Unit);
        } else {
            _resources.NoGoldSoundEffect();
            Debug.Log("Недостаточно денег!");
        }
        _resources.ShowRemainder();
    }
}
