using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class BaseBehavior : MonoBehaviour {
    public PlayerInventory playerInventory;
    public int life;
    public GameObject LifeText;

    private void Start() {
        LifeText = GameObject.FindWithTag("TextMoney");
        LifeText.GetComponent<Text>().text = life.ToString();
    }

    public void TakeDamage(int damage) {
        Debug.Log("OBA");
        life -= damage;
        LifeText.GetComponent<Text>().text = life.ToString();
    }
}
