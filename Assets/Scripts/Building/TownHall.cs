using UnityEngine;

public class TownHall : Building
{
    [SerializeField] private CoinsMiner _coinsMiner;
    
    public override void Select() {
        base.Select();
        CollectionPoint.SetActive(true);
    }

    public override void Unselect() {
        base.Unselect();
        CollectionPoint.SetActive(false);
    }

    public override void OnHover() {
        transform.localScale = Vector3.one * 1.01f;
    }

    public override void OnUnhover() {
        transform.localScale = Vector3.one;
    }

    public override void SetStage(StagesOfConstruction currentStage) {
        base.SetStage(currentStage);
        if (currentStage == StagesOfConstruction.Readiness) {
            _coinsMiner.gameObject.SetActive(true);
        } else {
            _coinsMiner.gameObject.SetActive(false);
        }
    }
}
