using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMarket : MonoBehaviour
{
    public List<Pig> pigTypes;
    public List<PigObject> playerPigs;
    public Vector3 pigSpawnPosition;
    private PlayerInventory _playerInventory;
    private PigPriceManager _pigPriceManager;
    // Start is called before the first frame update
    void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        _pigPriceManager = GetComponent<PigPriceManager>();
        foreach(Pig pigType in pigTypes)
        {
            pigType.SetCurrentPrice(pigType.initialPrice);//se no primeiro round e nao houver jogo salvo
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyPig(PigObject pig)
    {
        if (_playerInventory.money < pig.pigType.GetCurrentPrice())
            return;//avisar que o player nao tem dinheiro suficiente
        _playerInventory.money -= pig.pigType.GetCurrentPrice();
        PigObject pigObject = Instantiate(pig, pigSpawnPosition, Quaternion.identity);
        playerPigs.Add(pigObject);
    }

    public void SellPig(Pig pigType)
    {
        foreach(PigObject pig in playerPigs)
        {
            if (pig.pigType.Equals(pigType))
            {
                _playerInventory.money += pig.pigType.GetCurrentPrice();
                Destroy(pig);
            }
        }
    }

    


}
