using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    public BuildingPlacer BuildingPlacer;
    public Building Building;
    private Resources _resources;

    private void Start() {
       _resources = FindObjectOfType<Resources>();
    }

    public void TryBuy() {
        int price = Building.Price;
        
        if (_resources.Money >= price) {
            _resources.Money -= price;
            BuildingPlacer.CreateBuilding(Building);
        } else {
            _resources.NoGoldSoundEffect();
            Debug.Log("Недостаточно денег!");
        }
        _resources.ShowRemainder();
    }
}
