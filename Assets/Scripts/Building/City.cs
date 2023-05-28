using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public BuildingPlacer BuildingPlacer;
    public List<Building> CityBuildings = new List<Building>();

    private void Start() {
        if (CityBuildings.Count > 0) {
            foreach (var item in CityBuildings) {
                BuildingPlacer.InstallBuilding((int)item.transform.position.x, (int)item.transform.position.z, item);
            }
        }
    }
}
