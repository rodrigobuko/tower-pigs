using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigObject : MonoBehaviour
{
    public Pig pigType;

    public float ReturnMoney()//valores aleatorios, precisa de balanceamento
    {
        switch (pigType.investimentType)
        {
            case PigInvestimentType.HighRisk:
                return pigType.currentPrice*0.5f;
            case PigInvestimentType.MediumRisk:
                return pigType.currentPrice * 0.5f;
            default://smallRisk
                return pigType.currentPrice * 0.5f;
        }
    }
}
