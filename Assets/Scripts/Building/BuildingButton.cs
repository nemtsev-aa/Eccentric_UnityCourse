using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    public BuildingPlacer BuildingPlacer;
    public GameObject BuildingPrefab;

    public void TryBuy() {
        int price = BuildingPrefab.GetComponent<Building>().Price;
        Resources resources = FindObjectOfType<Resources>();
        if (resources.Money >= price) {
            FindObjectOfType<Resources>().Money -= price;
            BuildingPlacer.CreateBuilding(BuildingPrefab);
        } else {
            resources.NoGoldSoundEffect();
            Debug.Log("Недостаточно денег!");
        }
        resources.ShowRemainder();
    }
}
