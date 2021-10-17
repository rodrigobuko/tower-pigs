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
    private PigPriceManager _pigPriceManager;
    public Sprite PriceRiseSprite;
    public Sprite PriceDecreaseSprite;
    public GameObject MarketUI;
    public int round;
    void Start()
    {
        _pigMarket = GetComponent<PigMarket>();
        _pigPriceManager = GetComponent<PigPriceManager>();
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
        //PigPopUp.transform.GetChild(6).GetComponent<Image>().sprite = pigType.sprite;//imagem do porco
        PigPopUp.transform.GetChild(7).GetComponent<Text>().text = pigType.GetCurrentPrice().ToString()+" moedas";//preco atual  
        //PigPopUp.transform.GetChild(8).GetComponent<Image>().sprite = SetPriceChangeSprite(round, pigType);//imagem indicando se o preco aumentou ou diminuiu
        PigPopUp.transform.GetChild(9).GetComponent<Text>().text = _pigPriceManager.ReturnChangePercent(round, pigType).ToString() + " %";//;//porcentagem de mudança no preço

    }

    public void OpenMarket()
    {
        MarketUI.SetActive(true);
    }

    public void CloseMarket()
    {
        MarketUI.SetActive(false);
    }

    public void ClosePigPopUp()
    {
        PigPopUp.SetActive(false);
        sellButton.onClick.RemoveAllListeners();
        buyButton.onClick.RemoveAllListeners();
    }

    private Sprite SetPriceChangeSprite(int round, Pig pigType)
    {
        if (_pigPriceManager.CheckPriceChange( pigType ) == 1)
        {
            return PriceRiseSprite;
        }
        return PriceDecreaseSprite;
    }
}
