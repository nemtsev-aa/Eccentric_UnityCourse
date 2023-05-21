using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : PanelState
{
    [SerializeField] private GameObject _buildingPanel;

    public override void Init(PanelStateManager PanelStateManager) {
        
    }

    public override void EnterFirstTime() {
        
    }

    public override void Enter() {
        base.Enter();
        _buildingPanel.SetActive(true);
    }

    public override void Exit() {
        base.Exit();
        _buildingPanel.SetActive(false);
    }
}
