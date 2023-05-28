using UnityEngine;

public class PanelStateManager : MonoBehaviour
{
    [Tooltip("Менеджер объектов")]
    public Management Management;
    public Building SelectionBuilding;
    public Unit SelectionUnit;
    
    [Space(10)]
    [Tooltip("Состояние - Здания")]
    [SerializeField] private PanelState _buildingState;
    [Tooltip("Состояние - Замок")]
    [SerializeField] private PanelState _castleState;
    [Tooltip("Состояние - Ратуша")]
    [SerializeField] private PanelState _townHallState;
    [Tooltip("Состояние - Бараки")]
    [SerializeField] private PanelState _barrackState;
    [Tooltip("Состояние - Ферма")]
    [SerializeField] private PanelState _farmState;
    [Tooltip("Состояние - Панель строительных команд")]
    [SerializeField] private PanelState _constructionCommandState;
    [Tooltip("Состояние - Панель атакующих команд")]
    [SerializeField] private PanelState _attackCommandState;

    private PanelState _currentPanelState; // Текущее игровое состояние

    public void Init() {
        _buildingState?.Init(this);
        _castleState?.Init(this);
        _townHallState?.Init(this);
        _barrackState?.Init(this);
        _farmState?.Init(this);
        _constructionCommandState?.Init(this);
        _attackCommandState.Init(this);

        SetPanelState(_buildingState);
    }

    public void ShowBuildingPanel(Building building) {
        SelectionBuilding = building;
        switch (building.BuildingType) {
            case BuildingType.Castle:
                //Debug.Log("Barrack");
                SetCastle();
                break;
            case BuildingType.Barrack:
                //Debug.Log("Barrack");
                SetBarrack();
                break;
            case BuildingType.Farm:
                //Debug.Log("Farm");
                SetFarm();
                break;
            case BuildingType.TownHall:
                //Debug.Log("TownHall");
                SetTownHall();
                break;
            default:
                break;
        }
    }

    public void ShowUnitPanel(Unit unit) {
        SelectionUnit = unit;
        switch (unit.UnitType) {
            case UnitType.Knight:
                //Debug.Log("Knight");
                SetAttackCommand();
                break;
            case UnitType.Farmer:
                //Debug.Log("Farmer");
                SetConstructionCommandState();
                break;
            case UnitType.Builder:
                //Debug.Log("Builder");
                if (_currentPanelState != _buildingState) {
                    SetConstructionCommandState();
                }
                break;
            case UnitType.Scout:
                //Debug.Log("Scout");
                SetAttackCommand();
                break;
            default:
                break;
        }
    }

    private void SetPanelState(PanelState PanelState) {
        if (_currentPanelState) {
            _currentPanelState.Exit(); //Выходим из текущего состояния
        }
        _currentPanelState = PanelState; // Изменяем текущее состояние
        PanelState.Enter();  //Входим в новое состояние
    }

    [ContextMenu("SetBuilding")]
    public void SetBuilding() {
        SetPanelState(_buildingState);
    }

    [ContextMenu("SetTownHall")]
    public void SetTownHall() {
        SetPanelState(_townHallState);
    }

    [ContextMenu("SetCastle")]
    public void SetCastle() {
        SetPanelState(_castleState);
    }

    [ContextMenu("SetBarrack")]
    public void SetBarrack() {
        SetPanelState(_barrackState);
    }

    [ContextMenu("SetFarm")]
    public void SetFarm() {
        SetPanelState(_farmState);
    }

    [ContextMenu("SetAttackCommand")]
    public void SetAttackCommand() {
        SetPanelState(_attackCommandState);
    }

    [ContextMenu("SetConstructionCommand")]
    public void SetConstructionCommandState() {
        SetPanelState(_constructionCommandState);
    }
}
