using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionCommandState : PanelState
{
    [SerializeField] private GameObject _�onstructionCommandPanel;

    public override void Enter() {
        base.Enter();
        _�onstructionCommandPanel.SetActive(true);
    }

    public override void Exit() {
        base.Exit();
        _�onstructionCommandPanel.SetActive(false);
    }
}
