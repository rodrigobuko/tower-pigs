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
    public int damageOnBase;
    [NonSerialized] public EnemyGenerator enemyGenerator;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private SpriteRenderer lifeBar;
    private float maxLife;
    private float maxLifeBarWidth;
    // Start is called before the first frame update
    private void Awake() {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        maxLife = life;
        maxLifeBarWidth = lifeBar.size.x;
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
        lifeBar.size = new Vector2(maxLifeBarWidth * life / maxLife, lifeBar.size.y);
        if (life <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("e morreu...");
        enemyGenerator.enemyCount--;
        Destroy(gameObject);
    }

    private IEnumerator ReturnVelocity(float oldVelocity, float stunTime) {
        yield return new WaitForSecondsRealtime(stunTime);
        UpdateVelocity(oldVelocity, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Base")) {
            other.gameObject.GetComponent<BaseBehavior>().TakeDamage(damageOnBase);
            Die();
        }
    }
}
