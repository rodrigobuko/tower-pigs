using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Pig : ScriptableObject
{
    public string pigName;
    public string description;
    public float initialPrice;
    public Sprite sprite;
    public float _currentPrice;
    public float _previousPrice;
    private List<float> _pricesPerRound;
    //private List<int> _priceChangeHistory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCurrentPrice()
    {
        return _currentPrice;
    }

    public float GetPrice(int round)
    {
        return _pricesPerRound[round];
    }

    /*public int GetRoundPriceChange(int round)
    {
        return _priceChangeHistory[round];
    }*/

    public float GetPreviousPrice()
    {
        return _previousPrice;
    }

    public void SetCurrentPrice(float currentPrice)
    {
        _currentPrice = currentPrice;
        _pricesPerRound.Add(_currentPrice);
    }
    public void SetPreviousPrice(float previousPrice)
    {
        _previousPrice = previousPrice;
    }

    /*public void AddPriceChange(int changeCode)
    {
        _priceChangeHistory.Add(changeCode);
    }*/

    public void AddPrice(float price) { 
        _pricesPerRound.Add(price);
    }

}
