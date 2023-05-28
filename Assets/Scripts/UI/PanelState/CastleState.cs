using TMPro;
using UnityEngine;

public class CastleState : PanelState
{
    [SerializeField] private GameObject _castlePanel;
    [SerializeField] private TextMeshProUGUI _castleHealthValue;

    private Building _building;

    public override void Enter() {
        base.Enter();
        _building = _panelStateManager.SelectionBuilding;
        _building.OnHealth += ShowBuildingHealthValue;
        _building.ShowHealth();
        _castlePanel.SetActive(true);
    }

    public override void Exit() {
        base.Exit();
        _building.OnHealth -= ShowBuildingHealthValue;
        _castlePanel.SetActive(false);
    }

    private void ShowBuildingHealthValue(int currentHealth, int maxHealth) {
        _castleHealthValue.text = currentHealth + "/" + maxHealth;
    }
}
