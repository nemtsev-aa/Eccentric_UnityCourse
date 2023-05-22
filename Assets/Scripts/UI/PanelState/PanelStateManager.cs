using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelStateManager : MonoBehaviour
{
    [Tooltip("�������� ��������")]
    public Management Management;
    [Space(10)]
    [Tooltip("��������� - ������")]
    [SerializeField] private PanelState _buildingState;
    [Tooltip("��������� - ������")]
    [SerializeField] private PanelState _townHallState;
    [Tooltip("��������� - ������")]
    [SerializeField] private PanelState _barrackState;
    [Tooltip("��������� - �����")]
    [SerializeField] private PanelState _farmState;
    [Tooltip("��������� - ������ ������������ ������")]
    [SerializeField] private PanelState _constructionCommandState;
    [Tooltip("��������� - ������ ��������� ������")]
    [SerializeField] private PanelState _attackCommandState;

    private PanelState _currentPanelState; // ������� ������� ���������

    public void Init() {
        _buildingState?.Init(this);
        _townHallState?.Init(this);
        _barrackState?.Init(this);
        _farmState?.Init(this);
        _constructionCommandState?.Init(this);
        _attackCommandState.Init(this);

        SetGameState(_buildingState);
    }
    public void ShowBuildingPanel(GameObject selectedObject) {

        switch (selectedObject.GetComponent<Building>().BuildingType) {
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

    public void ShowUnitPanel(GameObject selectedObject) {

        switch (selectedObject.GetComponent<Unit>().UnitType) {
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

    private void SetGameState(PanelState PanelState) {
        if (_currentPanelState) {
            _currentPanelState.Exit(); //������� �� �������� ���������
        }
        _currentPanelState = PanelState; // �������� ������� ���������
        PanelState.Enter();  //������ � ����� ���������
    }

    [ContextMenu("SetBuilding")]
    public void SetBuilding() {
        SetGameState(_buildingState);
    }

    [ContextMenu("SetTownHall")]
    public void SetTownHall() {
        SetGameState(_townHallState);
    }

    [ContextMenu("SetBarrack")]
    public void SetBarrack() {
        SetGameState(_barrackState);
    }

    [ContextMenu("SetFarm")]
    public void SetFarm() {
        SetGameState(_farmState);
    }

    [ContextMenu("SetAttackCommand")]
    public void SetAttackCommand() {
        SetGameState(_attackCommandState);
    }

    [ContextMenu("SetConstructionCommand")]
    public void SetConstructionCommandState() {
        SetGameState(_constructionCommandState);
    }
}
