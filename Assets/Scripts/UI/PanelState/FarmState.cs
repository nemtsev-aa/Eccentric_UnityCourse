using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmState : PanelState {

    [SerializeField] private GameObject _farmPanel;

    public override void Enter() {
        base.Enter();
        _farmPanel.SetActive(true);
    }
    public override void Exit() {
        base.Exit();
        _farmPanel.SetActive(false);
    }
}
