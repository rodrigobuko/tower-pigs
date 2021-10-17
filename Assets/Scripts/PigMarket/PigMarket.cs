using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMarket : MonoBehaviour
{
    public List<Pig> pigTypes;
    public List<GameObject> playerPigs;
    public List<PigObject> pigPrefabs;
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

    public void BuyPig(GameObject pigWanted)
    {
        if (!pigWanted)
            return;
        Pig pigType = pigWanted.GetComponent<PigObject>().pigType;
        if (_playerInventory.money < pigType.GetCurrentPrice())
            return;//avisar que o player nao tem dinheiro suficiente
        _playerInventory.money -= pigType.GetCurrentPrice();
        GameObject pigObject = Instantiate(GetPigPrefab(pigType), pigSpawnPosition, Quaternion.identity);
        playerPigs.Add(pigObject);
    }

    public void SellPig(GameObject pigWanted)
    {
        if (!pigWanted)
            return;
        PigObject pigObjectWanted = pigWanted.GetComponent<PigObject>();
        foreach(GameObject pig in playerPigs)
        {
            Pig pigType = pig.GetComponent<PigObject>().pigType;
            if (pigType.Equals(pigObjectWanted.pigType))
            {
                _playerInventory.money += pigType.GetCurrentPrice();
                playerPigs.Remove(pig);
                Destroy(pig);
                return;
            }
        }
    }

    public GameObject GetPigPrefab(Pig pigWanted)
    {
        foreach(PigObject pig in pigPrefabs)
        {
            if (pig.pigType.Equals(pigWanted))
                return pig.gameObject;
        }
        return null;
    }

    


}
