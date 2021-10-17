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

    public void UpdateVelocity(float newVelocity) {
        velocity = newVelocity;
        navMeshAgent.speed = velocity;
    }
    
    public void UpdateLife(float newLife) {
        life = newLife;
        if (life <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("e morreu...");
    }
}
