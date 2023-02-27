using System;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPartsName
{
    Head,
    Body,
    Hands,
    Legs,
    Feet
}

public class BodyPartsSwitching : MonoBehaviour
{
    [SerializeField] private List<BodyPart> _bodyParts;
    [SerializeField] public int SelectionPartIndex;

    public static event Action<BodyPart> BodyPartSwitching;
    private void Start()
    {
        SelectPart(SelectionPartIndex);
    }

    public void NextPart()
    {
        DeselectPart(SelectionPartIndex);
        SelectionPartIndex++;

        if (SelectionPartIndex > _bodyParts.Count - 1)
        {
            SelectionPartIndex = 0;
        }

        SelectPart(SelectionPartIndex);
    }
    public void BackPart()
    {
        DeselectPart(SelectionPartIndex);
        SelectionPartIndex--;

        if (SelectionPartIndex < 0)
        {
            SelectionPartIndex = _bodyParts.Count - 1;
        }
        SelectPart(SelectionPartIndex);
    }

    private void SelectPart(int partIndex)
    {
        _bodyParts[partIndex].SetStatus(true);
        BodyPartSwitching?.Invoke(_bodyParts[partIndex]);
    }

    private void DeselectPart(int partIndex)
    {
        _bodyParts[partIndex].SetStatus(false);
    }

    public void SetScaleToBodyPart(float value)
    {
        _bodyParts[SelectionPartIndex].SetScale(value);
    }
    public void SetColorToBodyPart(Material value)
    {
        _bodyParts[SelectionPartIndex].SetColor(value);
    }
    
}


