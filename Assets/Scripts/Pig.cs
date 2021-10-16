using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Pig : ScriptableObject
{
    public string pigName;
    public string description;
    public float initialPrice;
    private float _currentPrice;
    private List<float> _pricesPerRound;
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
}
