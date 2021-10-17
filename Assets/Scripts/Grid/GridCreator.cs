using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[System.Serializable]
public struct RandomObjects {
    public int max;
    public int min;
}

public struct Position{
    public int row;
    public int col;
}
public class GridCreator : MonoBehaviour {
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private List<GameObject> randomObjectsList;
    [SerializeField] private GameObject city;
    [SerializeField] private int row;
    [SerializeField] private int column;
    [SerializeField] private RandomObjects randomObjects;
    [NonSerialized] public List<GameObject> grid ;
    private float padding = 1.0f;
    private GridCell[,] gridCellMat;
    void Start()
    {
        grid = new List<GameObject>();
        gridCellMat = new GridCell[row, column];
        CreateGrid();
        DeactivateGrid();
        FillTheGrid();
    }

    private void CreateGrid() {
        Vector3 position = transform.position;
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < column; j++) {
                var gridCell = Instantiate(gridCellPrefab);
                gridCell.transform.position = position;
                GridCell gridCellComponent = gridCell.GetComponent<GridCell>();
                gridCellComponent.row = i;
                gridCellComponent.column = j;
                gridCellMat[i, j] = gridCellComponent;
                position.x += padding;
                grid.Add(gridCell);
                
            }
            position.z -= padding;
            position.x = transform.position.x;;
        }
    }

    public void ActivateGrid() {
        foreach (var gridCell in grid) {
            gridCell.GetComponent<GridCell>().ActivatedCell();
        }
    }
    
    public void DeactivateGrid() {
        foreach (var gridCell in grid) {
            gridCell.GetComponent<GridCell>().DeactivateCell();
        }
    }

    private void FillTheGrid() {
        // place base 
        gridCellMat[row/2, column/2].occupied = true;
        gridCellMat[row/2, column/2 + 1].occupied = true;
        gridCellMat[row/2 + 1, column/2].occupied = true;
        gridCellMat[row/2 + 1, column/2 + 1].occupied = true;
        
        // place random objects 
        int random = UnityEngine.Random.Range(randomObjects.min, randomObjects.max);
        for (int i = 0; i < random; i++) {
            FindRandomPositions(0);
        }
    }

    private void FindRandomPositions(int attempt) {
        if (attempt > 5) {
            return;
        }
        int randomRow = UnityEngine.Random.Range(0, row - 1);
        int randomColumn = UnityEngine.Random.Range(0, column - 1);

        if (gridCellMat[randomRow, randomColumn].occupied) {
          FindRandomPositions(attempt + 1);
        } else {
          
            var randomObject = Instantiate(randomObjectsList[UnityEngine.Random.Range(0, randomObjectsList.Count)]);
            randomObject.transform.position = gridCellMat[randomRow, randomColumn].gameObject.transform.position;
            var numberOfGrids = randomObject.GetComponent<PlacebaleObject>().numberOfGrids;
            if(numberOfGrids == 1){
                gridCellMat[randomRow, randomColumn].occupied = true;
            } else {
                gridCellMat[randomRow, randomColumn].occupied = true;
                gridCellMat[randomRow, randomColumn + 1].occupied = true;
                gridCellMat[randomRow + 1, randomColumn].occupied = true;
                gridCellMat[randomRow + 1, randomColumn + 1].occupied = true;
            }
        }
        
    }
}
