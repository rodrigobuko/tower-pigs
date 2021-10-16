using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour {

    public bool occupied;
    public int row;
    public int column;
    
    public void DeactivateCell() {
        gameObject.SetActive(false);
    }

    public void ActivatedCell() {
        if (occupied) {
            // red 
        } else {
            // green 
            gameObject.SetActive(true);
        }
    }
}
