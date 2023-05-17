using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public GameObject SelectIndicator;

    public void Start() {
        SelectIndicator.SetActive(false);
    }

    public void OnHover() {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnUnhover() {
        transform.localScale = Vector3.one;
    }

    public virtual void Select() {
        SelectIndicator.SetActive(true);
    }

    public virtual void Unselect() {
        SelectIndicator.SetActive(false);
    }

    public virtual void WhenClickOnGround(Vector3 point) {

    }
}

