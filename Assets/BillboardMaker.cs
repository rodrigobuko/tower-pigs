using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardMaker : MonoBehaviour
{
    private Camera cam;
    public bool staticBillboard;
    
    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        if (!staticBillboard)
        {
            transform.LookAt(cam.transform);
        } 
        else
        {
            transform.rotation = cam.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
