using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [NonSerialized] public Vector3 followPoint;

    public float life;
    public float velocity;
    
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    private void Awake() {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void SetUpNavAgent() {
        
        navMeshAgent.SetDestination(followPoint);
        navMeshAgent.speed = velocity;
    }

    public void UpdateVelocity(float newVelocity, float stunTime) {
        float oldVelocity = velocity;
        velocity = newVelocity;
        navMeshAgent.speed = velocity;
        if (stunTime > 0) {
            StartCoroutine(ReturnVelocity(oldVelocity, stunTime));
        }
    }
    
    public void UpdateLife(float damage) {
        life -= damage;
        if (life <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("e morreu...");
        Destroy(gameObject);
    }

    private IEnumerator ReturnVelocity(float oldVelocity, float stunTime) {
        yield return new WaitForSecondsRealtime(stunTime);
        UpdateVelocity(oldVelocity, 0);

    }
}
