using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class PigMovement : MonoBehaviour {

    private Vector3 startPoint;
    private Vector3 endPoint;
    private float speed;
    [SerializeField] private Animator animator;
    private bool stoped;
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
        stoped = false;
    }    

        void Update () {
            if (!stoped) {
                if (gameObject.transform.position != startPoint) {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startPoint, speed);
                    gameObject.transform.LookAt(-startPoint);
                } else {
                    stoped = true;
                    startPoint = endPoint;
                    
                    float angle = Mathf.Deg2Rad * UnityEngine.Random.Range(0, 360);
                    var x = (float)(0.5f * Mathf.Cos(angle));
                    var z = (float)(0.5f * Mathf.Sin(angle));
                    endPoint = new Vector3(x, gameObject.transform.position.y, z);
                    StartCoroutine(PerformIdle());
                }
            }

        }

        private IEnumerator PerformIdle() {
            animator.SetBool("Idle", true);
            yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(1, 3));
            animator.SetBool("Idle", false);
            stoped = false;
        }
        
}
