using UnityEngine;

public class PanelState : MonoBehaviour
{
    [SerializeField] private GameObject _units;

    protected PanelStateManager _panelStateManager;
    protected UnitsTrainer _unitsTrainer;

    private bool _wasSet; // ������� ������ ��������� ���������

    public virtual void Init(PanelStateManager PanelStateManager) {
        _panelStateManager = PanelStateManager;
    }
    
    public virtual void EnterFirstTime() {
        //Debug.Log("EnterFirstTime:" + this.gameObject.name);
        if (_panelStateManager.Management.CurrentSelectionState == SelectionState.BuildingSelected) { // �������� ������
            _unitsTrainer = _panelStateManager.Management.ListOfSelected[0].gameObject.GetComponent<UnitsTrainer>();
            //Debug.Log("Step1: " + _unitsTrainer.gameObject.name);
            if (_unitsTrainer) { // ������ ����� ����������� ����������� ������
                foreach (Transform child in _units.GetComponentsInChildren<Transform>()) {
                    if (child.TryGetComponent(out TrainingButton trainingButton)) {
                        //Debug.Log("Step2: " + trainingButton.gameObject.name);
                        trainingButton.Init(_unitsTrainer); // ���������� ������ ��������������� ������
                    }
                }
            }
        }
    }

    public virtual void Enter() {
        if (!_wasSet) // ���� ��������� ������������ �������
        {
            EnterFirstTime();
            _wasSet = true;
        }
    }

    public virtual void Exit() {

    }
}
