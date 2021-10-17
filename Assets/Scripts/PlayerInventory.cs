using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public float money;
    [HideInInspector]public float price;
    public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        money = 500f;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString();
    }

    public void BuySomething()
    {
        money -= price;
    }
}
