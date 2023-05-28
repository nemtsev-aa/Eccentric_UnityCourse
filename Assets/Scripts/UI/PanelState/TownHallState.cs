using TMPro;
using UnityEngine;

public class TownHallState : PanelState 
{
    [SerializeField] private GameObject _townHallPanel;
    [SerializeField] private TextMeshProUGUI _townHallHealthValue;

    private Building _building;

    public override void Enter() {
        base.Enter();
        _building = _panelStateManager.SelectionBuilding;
        _building.OnHealth += ShowBuildingHealthValue;
        _building.ShowHealth();
        _townHallPanel.SetActive(true);
    }

    public override void Exit() {
        base.Exit();
        _building.OnHealth -= ShowBuildingHealthValue;
        _townHallPanel.SetActive(false);
    }

    private void ShowBuildingHealthValue(int currentHealth, int maxHealth) {
        _townHallHealthValue.text = currentHealth + "/" + maxHealth;
    }
}
