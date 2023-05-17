using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mamagement : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject Hovered;
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();

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

        if (Input.GetMouseButtonUp(0)) { // �������� ��������� ������
            if (Hovered) {
                if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftAlt))  {
                    UnselectAll();
                    Select(Hovered);
                }
            }

            if (hit.collider.tag == "Ground") {
                foreach (var iSelectedItem in ListOfSelected) {
                    iSelectedItem.WhenClickOnGround(hit.point);
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) { // ������� ������ ���������� �������� �������� �� RightMouse
            UnselectAll();
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
    }

    private void UnhowerCurrent() {
        if (Hovered) {
            Hovered.OnUnhover();
            Hovered = null;
        }
    }
}
