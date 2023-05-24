using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SelectionState {
    UnitsSelected,
    Frame,
    Other,
    BuildingSelected
}

public class Management : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject Hovered;
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();
    public SelectionState CurrentSelectionState;
    [Space(10)]
    public PanelStateManager PanelStateManager;

    [Header("Frame")]
    public Image FrameImage;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;

    private bool _isOverUI;
    private Plane _plane;

    void Update()
    {
        _isOverUI = EventSystem.current.IsPointerOverGameObject();

        Ray ray = Camera.ScreenPointToRay(Input.mousePosition); // ��� �� ������ � ����� ������������ ������� ���� �� ������
        Debug.DrawLine(ray.origin, ray.direction * 10f, Color.red); // ������������ ����

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            SelectableCollider selectable = hit.collider.GetComponent<SelectableCollider>();
            if (selectable) {
                SelectableObject hitSelectable = selectable.SelectableObject;
                if (Hovered) {
                    if (Hovered != hitSelectable) {
                        Hovered.OnUnhover();
                        Hovered = hitSelectable;
                        Hovered.OnHover();
                    }
                } else {
                    Hovered = hitSelectable;
                    Hovered.OnHover();
                }
            } else {
                UnhowerCurrent();
            }
        } else {
            UnhowerCurrent();
        }

        if (Input.GetMouseButtonDown(0)) {
            if (Hovered) {
                if (Input.GetKey(KeyCode.LeftControl)) { // ��������� ������ � ������ ���������� ������ LeftCtr+LeftMouse
                    Select(Hovered);
                }
                else if (Input.GetKey(KeyCode.LeftAlt)) { // ������� ������ �� ������ ���������� ������ LeftAlt+LeftMouse
                    Unselect(Hovered);
                }
            } else {
                if (CurrentSelectionState == SelectionState.BuildingSelected && !_isOverUI) {
                    Building building = ListOfSelected[0].GetComponent<Building>();
                    if (building.CollectionPoint) {
                        _plane = new Plane(Vector3.up, Vector3.zero);
                        float distance;
                        _plane.Raycast(ray, out distance);
                        Vector3 point = ray.GetPoint(distance);
                        building.CollectionPoint.transform.position = point;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) { 
            if (Hovered) {
                if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftAlt)) {
                    UnselectAll();
                    Select(Hovered);    // �������� ��������� ������
                }
            }
        }

        if (CurrentSelectionState == SelectionState.UnitsSelected) {
            if (Input.GetMouseButtonUp(0)) { 
                if (hit.collider.tag == "Ground" && !_isOverUI) { 
                    foreach (var iSelectedItem in ListOfSelected) {
                        iSelectedItem.WhenClickOnGround(hit.point, this); // ����� ����� ���������� ��� ����������� ������
                    }
                }
            }
        }
        
        if (Input.GetMouseButtonDown(1)) { // ������� ������ ���������� �������� �������� �� RightMouse
            UnselectAll();
        }

        CreatingFrame(); //��������� ������
    }

    void CreatingFrame() {
 
        if (Input.GetMouseButtonDown(0)) { 
            _frameStart = Input.mousePosition; // ��������� ��������� ��������� ����
        }

        if (Input.GetMouseButton(0)) {
            
            _frameEnd = Input.mousePosition; // ��������� �������� ��������� ���� �� ����� ���� ������ LeftMouse ������

            Vector2 min = Vector2.Min(_frameStart, _frameEnd); 
            Vector2 max = Vector2.Max(_frameStart, _frameEnd);
            Vector2 size = max - min; // ������ ���������� �������

            if (size.magnitude > 10) { // ���������� � ��������� ������������ �������� �����

                FrameImage.enabled = true; // ���������� �����
                FrameImage.rectTransform.anchoredPosition = min; // ��������� �����
                FrameImage.rectTransform.sizeDelta = size;  // ������� �����
                
                Rect rect = new Rect(min, size); // ������������� ������� ��� ��������� ��������

                UnselectAll(); // ������� ��������� �� ���� ��������
                Unit[] allUnits = FindObjectsOfType<Unit>(); // ������ ���� ������ �� �����
                for (int i = 0; i < allUnits.Length; i++) {
                    Vector2 screenPosition = Camera.WorldToScreenPoint(allUnits[i].transform.position); // ���������� ������� �������� �� ��������� ������
                    if (rect.Contains(screenPosition)) { 
                        Select(allUnits[i]); // �������� �������, ����������� ������ �����
                    }
                }
                CurrentSelectionState = SelectionState.Frame;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            FrameImage.enabled = false; // �������� �����
            if (ListOfSelected.Count == 0) {
                CurrentSelectionState = SelectionState.Other;
            } else if (ListOfSelected.Count == 1) {
                GameObject selectedObject = ListOfSelected[0].gameObject;
                if (selectedObject.GetComponent<Building>()) {
                    //Debug.Log("�������� ������");
                    CurrentSelectionState = SelectionState.BuildingSelected;
                    PanelStateManager.ShowBuildingPanel(selectedObject);
                }
                else if (selectedObject.GetComponent<Unit>()) {
                    //Debug.Log("������� ����");
                    CurrentSelectionState = SelectionState.UnitsSelected;
                    PanelStateManager.ShowUnitPanel(selectedObject);
                }
            } else {
                if (ListOfSelected.Count > 1) {
                    CurrentSelectionState = SelectionState.UnitsSelected;
                }
            }
        }
    }

    void Select(SelectableObject selectableObject) {
        if (!ListOfSelected.Contains(selectableObject)) {
            ListOfSelected.Add(selectableObject);
            selectableObject.Select();
        }
    }

    public void Unselect(SelectableObject selectableObject) {
        if (ListOfSelected.Contains(selectableObject)) {
            ListOfSelected.Remove(selectableObject);
            selectableObject.Unselect();
        }
    }

    void UnselectAll() {
        foreach (var iSelected in ListOfSelected) {
            iSelected.Unselect();
        }
        ListOfSelected.Clear();
        CurrentSelectionState = SelectionState.Other;
    }

    private void UnhowerCurrent() {
        if (Hovered) {
            Hovered.OnUnhover();
            Hovered = null;
        }
    }
}
