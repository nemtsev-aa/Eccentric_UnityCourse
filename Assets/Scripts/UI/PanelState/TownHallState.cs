using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallState : PanelState 
{
    [SerializeField] private GameObject _townHallPanel;

    public override void Enter() {
        base.Enter();
        _townHallPanel.SetActive(true);
    }

    public override void Exit() {
        base.Exit();
        _townHallPanel.SetActive(false);
    }
}
