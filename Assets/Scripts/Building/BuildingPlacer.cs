using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public Camera RaycastCamera;
    [field: SerializeField] public float CellSize { get; private set; }
    public Building CurrentBuilding;
    public Dictionary<Vector2Int, Building> BuildingDictionary = new Dictionary<Vector2Int, Building>();
    private Plane _plane;
    
    private void Start() {
        _plane = new Plane(Vector3.up, Vector3.zero);
    }
    
    private void Update() {

        if (!CurrentBuilding) {
            return;
        }

        Ray ray = RaycastCamera.ScreenPointToRay(Input.mousePosition);

        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance) / CellSize;

        int x = Mathf.RoundToInt(point.x);
        int z = Mathf.RoundToInt(point.z);

        CurrentBuilding.transform.position = new Vector3(x, 0f, z) * CellSize;
        if (CheckAllow(x, z, CurrentBuilding)) {
            CurrentBuilding.DisplayAcceptablePosition();
            if (Input.GetMouseButtonDown(0)) {
                InstallBuilding(x, z, CurrentBuilding);
                CurrentBuilding = null;
            }
        } else {
            CurrentBuilding.DisplayUnacceptablePosition();
        }
    }

    private bool CheckAllow(int xPosition, int ZPosition, Building building) {
        for (int x = 0; x < building.XSize; x++) {
            for (int z = 0; z < building.ZSize; z++) {
                Vector2Int coordinate = new Vector2Int(xPosition + x, ZPosition + z);
                if (BuildingDictionary.ContainsKey(coordinate)) {
                    return false;
                }
            }
        }
        return true;
    }

    private void InstallBuilding(int xPosition, int ZPosition, Building building) {
        for (int x = 0; x < building.XSize; x++) {
            for (int z = 0; z < building.ZSize; z++) {
                Vector2Int coordinate = new Vector2Int(xPosition + x, ZPosition + z);
                BuildingDictionary.Add(coordinate, CurrentBuilding);
            }
        }
    }

    public void CreateBuilding(GameObject buildingPrafab) {
        GameObject newBuilding = Instantiate(buildingPrafab);
        CurrentBuilding = newBuilding.GetComponent<Building>();
    }
}