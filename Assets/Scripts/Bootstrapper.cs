using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private PanelStateManager _panelStateManager;

    private void Awake()
    {
        _gameStateManager.Init();
        _panelStateManager.Init();
    }
}
