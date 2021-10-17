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
        StartPigTypes();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyPig(GameObject pigWanted)//o porco não está surgindo no cercadinho
    {
        if (!pigWanted)
            return;
        Pig pigType = pigWanted.GetComponent<PigObject>().pigType;
        if (_playerInventory.money < pigType.currentPrice)
            return;//avisar que o player nao tem dinheiro suficiente
        _playerInventory.money -= pigType.currentPrice;
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
                _playerInventory.money += pigType.currentPrice;
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

    public void StartPigTypes()
    {
        foreach (Pig pigType in pigTypes)
        {
            pigType.SetCurrentPrice(pigType.initialPrice);//se no primeiro round e nao houver jogo salvo
            int option = UnityEngine.Random.Range(0, 2);
            if (option == 0)
            {
                pigType.SetPreviousPrice(pigType.currentPrice * UnityEngine.Random.Range(0.1f, 0.98f));
            }
            else
                pigType.SetPreviousPrice(pigType.currentPrice * UnityEngine.Random.Range(1.1f, 1.98f));
        }
    }


    public void GetMoneyFromPigs()//rodar ao fim de cada rodada, para coletar o dinheiro das acoes
    {
        foreach (GameObject pig in playerPigs)
        {
            PigObject pigObject = pig.GetComponent<PigObject>();
            _playerInventory.money += pigObject.ReturnMoney();
            _playerInventory.pigMoney += pigObject.ReturnMoney();
        }
    }


}
