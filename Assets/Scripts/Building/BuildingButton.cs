using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    public BuildingPlacer BuildingPlacer;
    public GameObject BuildingPrefab;

    public void TryBuy() {
        int price = BuildingPrefab.GetComponent<Building>().Price;
        int monyCount = FindObjectOfType<Resources>().Money;
        if (monyCount >= price) {
            monyCount -= price;
            BuildingPlacer.CreateBuilding(BuildingPrefab);
        } else {
            Debug.Log("������������ �����!");
        }
        
    }
}
