using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class PigMovement : MonoBehaviour {

    private Vector3 startPoint;
    private Vector3 endPoint;
    private float speed;
    [SerializeField] private Animator animator;
    
    void Start () {
        animator.SetBool("Idle", false);
        float angle = Mathf.Deg2Rad * UnityEngine.Random.Range(0, 360);
        var x = (float)(0.5f * Mathf.Cos(angle));
        var z = (float)(0.5f * Mathf.Sin(angle));
        startPoint = new Vector3(x, gameObject.transform.position.y, z);
        angle = Mathf.Deg2Rad * UnityEngine.Random.Range(0, 360);
        x = (float)(0.5f * Mathf.Cos(angle));
        z = (float)(0.5f * Mathf.Sin(angle));
        endPoint = new Vector3(x, gameObject.transform.position.y, z);
        speed = UnityEngine.Random.Range(0.005f, 0.01f);
    }    

        void Update () {
            if (gameObject.transform.position != startPoint) {
                gameObject.transform.LookAt(-startPoint);
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startPoint, speed);
            } else {
                var aux = startPoint;
                startPoint = endPoint;
                endPoint = aux;
            }
        }


        
}
