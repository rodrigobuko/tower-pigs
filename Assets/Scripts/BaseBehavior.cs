using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehavior : MonoBehaviour {
    public PlayerInventory playerInventory;
    public void TakeDamage(int damage) {
        playerInventory.life -= damage;
    }
}
