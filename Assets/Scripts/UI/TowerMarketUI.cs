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
    public int round;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenTowerPopUp(TowerSkill towerSkill)
    {
        TowerPopUp.SetActive(true);
        buyButton.onClick.AddListener(delegate { /*passar função de posicionar a torre);*/ });
        //TowerPopUp.transform.GetChild(5).GetComponent<Text>().text = nome;
            //PigPopUp.transform.GetChild(6).GetComponent<Image>().sprite = pigType.sprite;//imagem do porco
            //TowerPopUp.transform.GetChild(7).GetComponent<Text>().text = preco().ToString() + " moedas";//preco atual  
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
}
