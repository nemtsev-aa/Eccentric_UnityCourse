using TMPro;
using UnityEngine;

public class BarrackState : PanelState
{
    [SerializeField] private GameObject _barrackPanel;
    [SerializeField] private TextMeshProUGUI _barrackHealthValue;

    private Building _building;
    
    public override void Enter() {
        base.Enter();
        _building = _panelStateManager.SelectionBuilding;
        _building.OnHealth += _building_OnHealth;
        _building.ShowHealth();
        _barrackPanel.SetActive(true);  
    }

    public override void Exit() {
        base.Exit();
        _building.OnHealth -= _building_OnHealth;
        _barrackPanel.SetActive(false);
    }

    private void _building_OnHealth(int currentHealth, int maxHealth) {
        _barrackHealthValue.text = currentHealth + "/" + maxHealth;
    }
}
