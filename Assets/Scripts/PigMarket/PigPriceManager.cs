using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PigPriceManager : MonoBehaviour
{
    public List<Pig> pigTypes;
    // Update is called once per frame
    void Update()
    {
        //mudar o preco dos porcos a cada rodada
    }

    private Pig getPigOfType(string pigTypeName)
    {
        foreach (Pig pigType in pigTypes)
        {
            if (pigType.pigName == pigTypeName)
                return pigType;
        }

        return null;
    }

    public void ChangePigsPrice()//versao muito simplificada de um possivel jeito de alterar o preco dos porcos; Rodar a cada fim de rodada
    {
        foreach (Pig pigType in pigTypes)
        {
            pigType.AddPrice(pigType.currentPrice);
            int option = UnityEngine.Random.Range(0, 2);
            if (option == 0)//preco diminui
            {
                pigType.SetPreviousPrice(pigType.currentPrice);
                switch (pigType.investimentType)
                {
                    case PigInvestimentType.HighRisk://acoes mais arriscadas podem ter queda de preco maior
                        pigType.SetCurrentPrice(pigType.currentPrice * UnityEngine.Random.Range(0.1f, 0.9f));
                        break;
                    case PigInvestimentType.MediumRisk:
                        pigType.SetCurrentPrice(pigType.currentPrice * UnityEngine.Random.Range(0.5f, 0.9f));
                        break;
                    default://smallRisk
                        pigType.SetCurrentPrice(pigType.currentPrice * UnityEngine.Random.Range(0.7f, 0.9f));
                        break;
                }
            }
            else//preco aumenta
            {
                pigType.SetPreviousPrice(pigType.currentPrice);
                switch (pigType.investimentType)//acoes mais arriscadas podem ter aumentos de preço maior
                {
                    case PigInvestimentType.HighRisk:
                        pigType.SetCurrentPrice(pigType.currentPrice * UnityEngine.Random.Range(1.1f, 1.9f));
                        break;
                    case PigInvestimentType.MediumRisk:
                        pigType.SetCurrentPrice(pigType.currentPrice * UnityEngine.Random.Range(1.1f, 1.5f));
                        break;
                    default://smallRisk
                        pigType.SetCurrentPrice(pigType.currentPrice * UnityEngine.Random.Range(1.1f, 1.3f));
                        break;
                }
            }
        }
    }

    public int CheckPriceChange( Pig pigType)
    {
        if (pigType.currentPrice - pigType.previousPrice > 0)
            return 1;
        else
            return -1;
    }

    public float ReturnChangePercent(int round, Pig pigType)
    {
        float oldPrice;
        if (round == 0 && pigType.previousPrice==0)
        {
            return CheckPriceChange(pigType) * UnityEngine.Random.Range(0f, 100f);
        }
        oldPrice = pigType.previousPrice;
        return (pigType.currentPrice - oldPrice) / 100;
    }



}
