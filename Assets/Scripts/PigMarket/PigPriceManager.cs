using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PigPriceManager : MonoBehaviour
{
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

    private Pig getPigOfType(string pigTypeName)
    {
        foreach (Pig pigType in pigTypes)
        {
            if (pigType.pigName == pigTypeName)
                return pigType;
        }

        return null;
    }

    public void ChangePigsPrice()
    {
        foreach (Pig pigType in pigTypes)
        {
            pigType.AddPrice(pigType.GetCurrentPrice());
            int option = UnityEngine.Random.Range(0, 2);
            if (option == 0)//preco diminui
            {
                pigType.SetPreviousPrice(pigType.GetCurrentPrice());
                pigType.SetCurrentPrice(pigType.GetCurrentPrice()*UnityEngine.Random.Range(0.1f, 0.9f));
            }
            else//preco aumenta
            {
                pigType.SetPreviousPrice(pigType.GetCurrentPrice());
                pigType.SetCurrentPrice(pigType.GetCurrentPrice() * UnityEngine.Random.Range(1.1f, 1.9f));
            }
        }
    }

    public int CheckPriceChange( Pig pigType)
    {
        if (pigType.GetCurrentPrice() - pigType.GetPreviousPrice() > 0)
            return 1;
        else
            return -1;
    }

    public float ReturnChangePercent(int round, Pig pigType)
    {
        float oldPrice;
        if (round == 0 && pigType.GetPreviousPrice()==0)
        {
            return CheckPriceChange(pigType) * UnityEngine.Random.Range(0f, 100f);
        }
        oldPrice = pigType.GetPreviousPrice();
        return (pigType.GetCurrentPrice() - oldPrice) / 100;
    }



}