using UnityEngine;

public class PanelState : MonoBehaviour
{
    [SerializeField] private GameObject _units;

    protected PanelStateManager _panelStateManager;
    protected UnitsTrainer _unitsTrainer;

    private bool _wasSet; // Отметка первой активации состояния

    public virtual void Init(PanelStateManager PanelStateManager) {
        _panelStateManager = PanelStateManager;
    }
    
    public virtual void EnterFirstTime() {
        //Debug.Log("EnterFirstTime:" + this.gameObject.name);
        if (_panelStateManager.Management.CurrentSelectionState == SelectionState.BuildingSelected) { // Выделено здание
            _unitsTrainer = _panelStateManager.Management.ListOfSelected[0].gameObject.GetComponent<UnitsTrainer>();
            //Debug.Log("Step1: " + _unitsTrainer.gameObject.name);
            if (_unitsTrainer) { // Здание имеет возможность приозводить юнитов
                foreach (Transform child in _units.GetComponentsInChildren<Transform>()) {
                    if (child.TryGetComponent(out TrainingButton trainingButton)) {
                        //Debug.Log("Step2: " + trainingButton.gameObject.name);
                        trainingButton.Init(_unitsTrainer); // Присваиаем кнопке соответствующее здание
                    }
                }
            }
        }
    }

    public virtual void Enter() {
        if (!_wasSet) // Если состояние активируется впервые
        {
            EnterFirstTime();
            _wasSet = true;
        }
    }

    public virtual void Exit() {

    }
}
