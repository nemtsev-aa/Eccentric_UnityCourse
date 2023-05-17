using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mamagement : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject Hovered;
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();

    [Header("Frame")]
    public Image FrameImage;

    private Vector2 _frameStart;
    private Vector2 _frameEnd;

    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition); // Луч из камеры в точку расположения курсора мыши на экране
        Debug.DrawLine(ray.origin, ray.direction * 10f, Color.red); // Визуализация луча

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
                if (Input.GetKey(KeyCode.LeftControl)) { // Добавляем объект в группу сочитанием клавиш LeftCtr+LeftMouse
                    Select(Hovered);
                }
                else if (Input.GetKey(KeyCode.LeftAlt)) { // Удаляем объект из группы сочитанием клавиш LeftAlt+LeftMouse
                    Unselect(Hovered);
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) { // Выделяем одиночный объект
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

        if (Input.GetMouseButtonDown(1)) { // Очищаем список выделенных объектов нажатием на RightMouse
            UnselectAll();
        }

        CreatingFrame(); //Выделение рамкой


    }

    void CreatingFrame() {
        
        if (Input.GetMouseButtonDown(0)) {
            _frameStart = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)) {
            
            _frameEnd = Input.mousePosition;

            Vector2 min = Vector2.Min(_frameStart, _frameEnd);
            Vector2 max = Vector2.Max(_frameStart, _frameEnd);
            Vector2 size = max - min;

            if (size.magnitude > 10) {
                FrameImage.enabled = true;

                FrameImage.rectTransform.anchoredPosition = min;
                FrameImage.rectTransform.sizeDelta = size;

                Rect rect = new Rect(min, size);

                UnselectAll();
                Unit[] allUnits = FindObjectsOfType<Unit>();
                for (int i = 0; i < allUnits.Length; i++) {
                    Vector2 screenPosition = Camera.WorldToScreenPoint(allUnits[i].transform.position);
                    if (rect.Contains(screenPosition)) {
                        Select(allUnits[i]);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            FrameImage.enabled = false;
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
