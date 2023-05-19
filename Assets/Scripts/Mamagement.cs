using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionState {
    UnitsSelected,
    Frame,
    Other
}

public class Mamagement : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject Hovered;
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();
    public SelectionState CurrentSelectionState;

    [Header("Frame")]
    public Image FrameImage;

    private Vector2 _frameStart;
    private Vector2 _frameEnd;

    void Update()
    {
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
            }
        }

        if (Input.GetMouseButtonUp(0)) { 
            if (Hovered) {
                if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftAlt)) {
                    UnselectAll();
                    Select(Hovered);    // �������� ��������� ������
                    CurrentSelectionState = SelectionState.UnitsSelected;
                }
            }
        }

        if (CurrentSelectionState == SelectionState.UnitsSelected) {
            if (Input.GetMouseButtonUp(0)) { 
                if (hit.collider.tag == "Ground") { 
                    foreach (var iSelectedItem in ListOfSelected) {
                        iSelectedItem.WhenClickOnGround(hit.point); // ����� ����� ���������� ��� ����������� ������
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
            Debug.Log("Input.mousePosition: " + Input.mousePosition);

        }

        if (Input.GetMouseButton(0)) {

            Debug.Log("_frameStart: " + _frameStart);

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
            if (ListOfSelected.Count > 0) {
                CurrentSelectionState = SelectionState.UnitsSelected;
            } else {
                CurrentSelectionState = SelectionState.Other;
            }
        }
    }

    void Select(SelectableObject selectableObject) {
        if (!ListOfSelected.Contains(selectableObject)) {
            ListOfSelected.Add(selectableObject);
            selectableObject.Select();
        }
    }

    void Unselect(SelectableObject selectableObject) {
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
