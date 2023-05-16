using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public void OnHover() {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnUnHover() {
        transform.localScale = Vector3.one;
    }
}
