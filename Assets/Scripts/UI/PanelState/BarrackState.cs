using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackState : PanelState
{
    [SerializeField] private GameObject _barrackPanel;

    public override void Enter() {
        base.Enter();
        _barrackPanel.SetActive(true);
    }

    public override void Exit() {
        base.Exit();
        _barrackPanel.SetActive(false);
    }
}
