using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PigInvestimentType
{
    HighRisk,
    MediumRisk,
    LowRisk
}
[CreateAssetMenu]
public class Pig : ScriptableObject
{
    public string pigName;
    public string description;
    public float initialPrice;
    public Sprite sprite;
    public float currentPrice;
    public float previousPrice;
    public PigInvestimentType investimentType;
    private List<float> _pricesPerRound;

    public float GetPrice(int round)
    {
        return _pricesPerRound[round];
    }

    public void SetCurrentPrice(float currentPrice)
    {
        this.currentPrice = currentPrice;
        _pricesPerRound.Add(currentPrice);
    }
    public void SetPreviousPrice(float previousPrice)
    {
        this.previousPrice = previousPrice;
    }

    public void AddPrice(float price) { 
        _pricesPerRound.Add(price);
    }

}
