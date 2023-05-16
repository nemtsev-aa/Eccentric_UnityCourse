using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mamagement : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject Howered;

    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition); // ��� �� ������ � ����� ������������ ������� ���� �� ������
        Debug.DrawLine(ray.origin, ray.direction * 10f, Color.red); // ������������ ����

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            SelectableCollider selectable = hit.collider.GetComponent<SelectableCollider>();
            if (selectable) {
                SelectableObject hitSelectable = selectable.SelectableObject;
                if (Howered) {
                    if (Howered != hitSelectable) {
                        Howered.OnUnhover();
                        Howered = hitSelectable;
                        Howered.OnHover();
                    }
                } else {
                    Howered = hitSelectable;
                    Howered.OnHover();
                }
            } else {
                UnhowerCurrent();
            }
        } else {
            UnhowerCurrent();
        }
    }

    private void UnhowerCurrent() {
        if (Howered) {
            Howered.OnUnhover();
            Howered = null;
        }
    }
}
