using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {
    Stoped,
    PlacingTower,
    EnemyWave
}
public class GameLogic : MonoBehaviour {
    
    [SerializeField] private GridCreator gridCreator;
    [SerializeField] private GameObject turret;
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
}
