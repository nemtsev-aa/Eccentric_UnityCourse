using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    //public static BuildingPlacer Instance;
    public Camera RaycastCamera;
    [field: SerializeField] public float CellSize { get; private set; }
    public Building CurrentBuilding;

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

        if (Input.GetMouseButtonDown(0)) {
            CurrentBuilding = null;
        }
    }

    public void CreateBuilding(GameObject buildingPrafab) {
        GameObject newBuilding = Instantiate(buildingPrafab);
        CurrentBuilding = newBuilding.GetComponent<Building>();
    }

}
