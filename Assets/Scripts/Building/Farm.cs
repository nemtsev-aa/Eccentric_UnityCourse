using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    public override void Select() {
        base.Select();
        CollectionPoint.SetActive(true);
    }

    public override void Unselect() {
        base.Unselect();
        CollectionPoint.SetActive(false);
    }

    public override void OnHover() {
        transform.localScale = Vector3.one * 1.01f;
    }

    public override void OnUnhover() {
        transform.localScale = Vector3.one;
    }
}
