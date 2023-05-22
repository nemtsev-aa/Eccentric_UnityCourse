using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommandState : PanelState
{
    [SerializeField] private GameObject _attackCommandPanel;

    public override void Enter() {
        base.Enter();
        _attackCommandPanel.SetActive(true);
    }

    public override void Exit() {
        base.Exit();
        _attackCommandPanel.SetActive(false);
    }
}
