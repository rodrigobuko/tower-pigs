using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PigMarketUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PigPopUp;
    public Button sellButton;
    public Button buyButton;
    public GameObject OpenMarketButton;
    private PigMarket _pigMarket;
    public Sprite PriceRiseSprite;
    public Sprite PriceDecreaseSprite;
    public GameObject MarketUI;
    void Start()
    {
        _pigMarket = GetComponent<PigMarket>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPigPopUp(Pig pigType)
    {
        PigPopUp.SetActive(true);
        sellButton.onClick.AddListener(delegate { _pigMarket.SellPig(_pigMarket.GetPigPrefab(pigType)); });
        buyButton.onClick.AddListener(delegate { _pigMarket.BuyPig(_pigMarket.GetPigPrefab(pigType)); });
        PigPopUp.transform.GetChild(5).GetComponent<Text>().text = pigType.pigName;
        //PigPopUp.transform.GetChild(9).GetComponent<Image>().sprite = pigType.sprite;
        PigPopUp.transform.GetChild(7).GetComponent<Text>().text = pigType.GetCurrentPrice().ToString()+" moedas";
        //PigPopUp.transform.GetChild(8).GetComponent<Text>().text = pigType.GetCurrentPrice().ToString();
        //PigPopUp.transform.GetChild(10).GetComponent<Image>().sprite =;

    }

    public void OpenMarket()
    {
        MarketUI.SetActive(true);
    }

    public void ClosePigPopUp()
    {
        PigPopUp.SetActive(false);
        sellButton.onClick.RemoveAllListeners();
        buyButton.onClick.RemoveAllListeners();
    }
}
