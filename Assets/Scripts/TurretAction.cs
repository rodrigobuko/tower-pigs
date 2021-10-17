using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretAction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TowerSkill towerSkill;
    private List<Enemy> enemiesAffected;
    private bool isAttacking;

    private void Start() {
        enemiesAffected = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckEnemyInRange()) {
            if (!isAttacking) {
                isAttacking = true;
                StartCoroutine(PerformAttack());
            }
            
        }
    }

    private bool CheckEnemyInRange() {
        var enemyFound = false;
        var hits = Physics.SphereCastAll(transform.position, towerSkill.range, Vector3.forward, 10, LayerMask.GetMask("Enemy"));
        if (hits.Length > 0) {
            enemiesAffected.Clear();
            foreach (var hit in hits) {
                if (hit.collider.gameObject.GetComponent<Enemy>() != null) {
                    enemyFound = true;
                    enemiesAffected.Add(hit.collider.gameObject.GetComponent<Enemy>());
                }
            }
        }
        return enemyFound; 
    }


    private void Attack(Enemy enemy) {
        enemy.UpdateLife(towerSkill.damage);
        enemy.UpdateVelocity(towerSkill.velocityReduce);
    }
    
    private void AttackBullet(Enemy enemy) {
        var bulletObject = Instantiate(towerSkill.bullet);
        bulletObject.transform.position = gameObject.transform.position;
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.myTower = towerSkill;
        bullet.target = enemy.gameObject;
        bullet.activated = true;
    }

    private IEnumerator PerformAttack() {
        if (towerSkill.singleTarget) {
            AttackBullet(enemiesAffected.First());
        } else {
            foreach (var enemy in enemiesAffected) {
                Attack(enemy);
            }
        }
        yield return new WaitForSecondsRealtime(towerSkill.cooldown);
        isAttacking = false; 
    }
    
}
