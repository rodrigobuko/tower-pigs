using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretAction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TowerSkill towerSkill;
    [SerializeField] private GameObject towerRotation;
    [SerializeField] private GameObject bulletPoint;
    private List<Enemy> enemiesAffected;
    private List<GameObject> currentBullets;
    private bool isAttacking;
    private bool notDetected;
    
    private void Start() {
        enemiesAffected = new List<Enemy>();
        currentBullets = new List<GameObject>();
        notDetected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (notDetected) {
            if (CheckEnemyInRange()) {
                if (!isAttacking) {
                    isAttacking = true;
                    if (towerSkill.delay > 0) {
                        StartCoroutine(PerformDelay());
                    } else {
                        StartCoroutine(PerformAttack(enemiesAffected));
                    }
                   
                }
            } else {
                ResetState();
            }
        }

    }

    private IEnumerator PerformDelay() {
        yield return new WaitForSecondsRealtime(towerSkill.delay);
        if (CheckEnemyInRange()) {
            Debug.Log("OBA");
            StartCoroutine(PerformAttack(enemiesAffected));
        }
        
    }
    private bool CheckEnemyInRange() {
        var enemyFound = false;
        var hits = Physics.OverlapSphere(transform.position, towerSkill.range);
        if (hits.Length > 0) {
            enemiesAffected.Clear();
            foreach (var hit in hits) {
                if (hit.gameObject.GetComponent<Enemy>() != null) {
                    enemyFound = true;
                    notDetected = false;
                    
                    enemiesAffected.Add(hit.gameObject.GetComponent<Enemy>());
                }
            }
        }
        return enemyFound; 
    }


    private void Attack(Enemy enemy) {
        enemy.UpdateLife(towerSkill.damage);
        enemy.UpdateVelocity(towerSkill.velocityReduce, towerSkill.stunTime);
    }
    
    private void AttackBullet(Enemy enemy) {
        if (towerRotation != null) {
            towerRotation.transform.LookAt(enemy.transform, Vector3.up);
            towerRotation.transform.rotation = Quaternion.Euler(0f, towerRotation.transform.rotation.eulerAngles.y, 0f);
        }
        var bulletObject = Instantiate(towerSkill.bullet);
        if (bulletPoint != null) {
            bulletObject.transform.position = bulletPoint.transform.position;
        } else {
            bulletObject.transform.position = gameObject.transform.position;
        }
        currentBullets.Add(bulletObject);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.myTower = towerSkill;
        bullet.target = enemy.gameObject;
        bullet.activated = true;
    }

    private IEnumerator PerformAttack(List<Enemy> enemies) {
        if (towerSkill.singleTarget) {
            AttackBullet(enemies.First());
        } else {
            foreach (var enemy in enemies) {
                Attack(enemy);
            }
        }
        yield return new WaitForSecondsRealtime(towerSkill.cooldown);
        isAttacking = false;
        notDetected = true; 
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,towerSkill.range);
    }

    private void ResetState() {
        foreach (var bullet in currentBullets) {
            Destroy(bullet);
        }
        currentBullets.Clear();
    }
}
