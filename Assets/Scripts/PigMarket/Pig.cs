using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KindOfPig{
    lowRisk,
    highRisk
}
[CreateAssetMenu]
public class Pig : ScriptableObject
{
    public string pigName;
    public string description;
    public float initialPrice;
    public KindOfPig kindOfPig;
    public Sprite sprite;
    private float _currentPrice;
    private List<float> _pricesPerRound;
    private List<float> _priceHistory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetPigPrice(int round)
    {
        return _pricesPerRound[round];
    }

    public float GetCurrentPrice()
    {
        return _currentPrice;
    }

    public void SetCurrentPrice(float currentPrice)
    {
        _currentPrice = currentPrice;
    }

    public void AddPriceChange(int changeCode)
    {
        _priceHistory.Add(changeCode);
    }

    public void AddPrice(float price) { 
        _priceHistory.Add(price);
    }

}
