using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GridCreator : MonoBehaviour {
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject city;
    [SerializeField] private int row;
    [SerializeField] private int column;

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
        int random = UnityEngine.Random.Range(2, 5);
    }
}
