using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GameState {
    Stoped,
    Preparation,
    PlacingTower,
    EnemyWave
}
[System.Serializable]
public enum TowerTypes
{
    Stun,
    Shoot,
    Laser
}
public class GameLogic : MonoBehaviour {
    
    [SerializeField] private GridCreator gridCreator;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject stunPrefab;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private GameObject laserPrefab;
    
    [SerializeField] private UnityEvent StartPreparationEvent;
    [SerializeField] private UnityEvent StartEnemyWaveEvent;
    [SerializeField] private Text StateText;
    [SerializeField] private Button StateButton;
    private GameState currentState;

    private void Start() {
        currentState = GameState.Stoped;
        StateText.text = "Iniciar Preparação";
    }

    public void  ActivateTowerInterface() {
        if (currentState == GameState.Preparation) {
            gridCreator.ActivateGrid();
            currentState = GameState.PlacingTower;
        }
    }

    public void StartWave() {
        if (currentState == GameState.Stoped) {
            currentState = GameState.EnemyWave;
        }
    }

    public void ChangeState() {
        switch (currentState) {
            case GameState.Stoped:
                StartPreparationEvent.Invoke();
                currentState = GameState.Preparation;
                StateText.text = "Ataque";
                break;
            case GameState.Preparation:
                StartEnemyWaveEvent.Invoke();
                currentState = GameState.EnemyWave;
                StateText.text = "Finalizar Turno";
                AudioManager.instance.Stop("Menu");
                AudioManager.instance.Play("Game");
                StateButton.interactable = false;
                break;
            case GameState.EnemyWave:
                currentState = GameState.Stoped;
                StateText.text = "Iniciar Preparação";
                StateButton.interactable = true;
                AudioManager.instance.Stop("Game");
                AudioManager.instance.Play("Menu");
                break;
        }
    }

    void Update()
    {
        if (currentState == GameState.PlacingTower) {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray;
                RaycastHit hit;
            
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.GetComponent<GridCell>() != null) {
                        PlaceTower(hit.collider.gameObject.GetComponent<GridCell>());
                    }
                }
            }
        }
    }

    private void PlaceTower(GridCell gridCell) {
        var turretObject = Instantiate(turret);
        turretObject.transform.position = gridCell.transform.position;
        gridCell.occupied = true;
        AudioManager.instance.Play("Construct");
        gridCreator.DeactivateGrid();
        currentState = GameState.Preparation;
    }

    public void ChooseTower(int towerType)
    {
        switch (towerType)
        {
            case 0:
                turret = laserPrefab;
                break;
            case 1:
                turret = shootPrefab;
                break;
            default:
                turret = stunPrefab;
                break;
        }
    }
}
