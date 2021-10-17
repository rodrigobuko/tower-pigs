using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [NonSerialized] public TowerSkill myTower;
    [NonSerialized] public GameObject target;
    [NonSerialized] public bool activated;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.UpdateLife(myTower.damage);
            enemy.UpdateVelocity(myTower.velocityReduce);
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (activated) {
            if (target != null) {
                gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.05f);
            }
        }
    }
}
