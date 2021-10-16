using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigPriceManager : MonoBehaviour
{
    [HideInInspector] public int round;
    public List<Pig> pigTypes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mudar o preco dos porcos a cada rodada
    }

    private int CalculatePigPriceChange(string pigTypeName)//retorna -1 se diminuiu; 0 se igual;//1 se aumentou
    {
        if (round == 0)
            return 0;
        Pig pig = getPigOfType(pigTypeName);

        if (pig)//nao ha porcos desse tipo
            return 0;

        float priceChange = pig.GetCurrentPrice() - pig.GetPigPrice(round);
        if (priceChange > 0)
            return 1;
        else if (priceChange < 0)
            return -1;
        else
            return 0;
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

}
