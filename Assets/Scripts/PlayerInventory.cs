using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public float money;
    public float pigMoney;
    public int life;
    [HideInInspector]public float price;
    public Text moneyText;
    public Text moneyText2;
    // Start is called before the first frame update
    void Start()
    {
        money = 500f;
        pigMoney = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString();
        moneyText2.text = money.ToString();
    }

    public void BuySomething()
    {
        money -= price;
    }

}
