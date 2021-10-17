using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    
    [SerializeField] private GameObject TowerStoreButton;
    [SerializeField] private GameObject PigStoreButton;

    // pigs
    [SerializeField] private PigPriceManager pigPriceManager;
    [SerializeField] private PigMarket pigMarket;
    private GameState currentState;

    private void Start() {
        currentState = GameState.Stoped;
        StateText.text = "Iniciar Preparação";
        PigStoreButton.SetActive(true);
        TowerStoreButton.SetActive(true);
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
                PigStoreButton.SetActive(false);
                TowerStoreButton.SetActive(false);
                break;
            case GameState.Preparation:
                StartEnemyWaveEvent.Invoke();
                currentState = GameState.EnemyWave;
                StateText.text = "Batalha";
                AudioManager.instance.Stop("Menu");
                AudioManager.instance.Play("Game");
                PigStoreButton.SetActive(true);
                TowerStoreButton.SetActive(true);
                StateButton.interactable = false;
                break;
            case GameState.EnemyWave:
                currentState = GameState.Stoped;
                StateText.text = "Iniciar Preparação";
                StateButton.interactable = true;
                PigStoreButton.SetActive(true);
                TowerStoreButton.SetActive(true);
                AudioManager.instance.Stop("Game");
                AudioManager.instance.Play("Menu");
                
                pigPriceManager.ChangePigsPrice();
                pigMarket.GetMoneyFromPigs();
                AudioManager.instance.Play("Coin");
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

    public void GameOver() {
        SceneManager.LoadScene("MENU");
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
