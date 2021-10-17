using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMarketUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TowerPopUp;
    public Button buyButton;
    public GameObject OpenMarketButton;
    public GameObject MarketUI;
    public GameLogic gameLogic;
    private PlayerInventory _playerInventory;
    public int round;
    void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenTowerPopUp(TowerSkill towerSkill)
    {
        TowerPopUp.SetActive(true);
        //TowerPopUp.transform.GetChild(4).GetComponent<Text>().text = nome;
        //PigPopUp.transform.GetChild(5).GetComponent<Image>().sprite = pigType.sprite;//imagem do porco
        TowerPopUp.transform.GetChild(6).GetComponent<Text>().text = towerSkill.price.ToString() + " moedas";//preco atual  
        if (towerSkill.price > _playerInventory.money)
            buyButton.enabled = false;
        else
        {
            buyButton.enabled = true;
            _playerInventory.price = towerSkill.price;
        }
            

        //TowerPopUp.transform.GetChild(7).GetComponent<Text>().text = descricao;//descricao
    }

    public void OpenMarket()
    {
        MarketUI.SetActive(true);
    }

    public void CloseTowerPopUp()
    {
        TowerPopUp.SetActive(false);
        buyButton.onClick.RemoveAllListeners();
    }

    public void CloseMarket()
    {
        MarketUI.SetActive(false);
    }
}
