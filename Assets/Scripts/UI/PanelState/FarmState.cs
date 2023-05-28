using TMPro;
using UnityEngine;

public class FarmState : PanelState {

    [SerializeField] private GameObject _farmPanel;
    [SerializeField] private TextMeshProUGUI _farmHealthValue;

    private Building _building;

    public override void Enter() {
        base.Enter();
        _building = _panelStateManager.SelectionBuilding;
        _building.OnHealth += _building_OnHealth;
        _building.ShowHealth();

        _farmPanel.SetActive(true);
    }
    public override void Exit() {
        base.Exit();
        _building.OnHealth -= _building_OnHealth;
        _farmPanel.SetActive(false);
    }

    private void _building_OnHealth(int currentHealth, int maxHealth) {
        _farmHealthValue.text = currentHealth + "/" + maxHealth;
    }
}
