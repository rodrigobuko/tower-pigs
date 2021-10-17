using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PigButton : MonoBehaviour
{
    public Pig pigType;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = pigType.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
