using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionCommandState : PanelState
{
    [SerializeField] private GameObject _ñonstructionCommandPanel;

    public override void Enter() {
        base.Enter();
        _ñonstructionCommandPanel.SetActive(true);
    }

    public override void Exit() {
        base.Exit();
        _ñonstructionCommandPanel.SetActive(false);
    }
}
