using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {
    Stoped,
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

    private GameState currentState;
    public void  ActivateTowerInterface() {
        if (currentState == GameState.Stoped) {
            gridCreator.ActivateGrid();
            currentState = GameState.PlacingTower;
        }
    }

    public void StartWave() {
        if (currentState == GameState.Stoped) {
            currentState = GameState.EnemyWave;
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
        gridCreator.DeactivateGrid();
        currentState = GameState.Stoped;
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
