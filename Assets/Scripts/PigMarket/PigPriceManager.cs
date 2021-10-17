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
                pigType.SetCurrentPrice(pigType.GetCurrentPrice()*UnityEngine.Random.Range(0.3f, 0.9f));
                pigType.AddPriceChange(-1);
            }
            else//preco aumenta
            {
                pigType.SetCurrentPrice(pigType.GetCurrentPrice() * UnityEngine.Random.Range(1.1f, 1.9f));
                pigType.AddPriceChange(1);
            }
        }
    }



}
