using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerSkill : ScriptableObject {
    public float cooldown;
    public float damage;
    public float range;
    public float velocityReduce;
    public float stunTime;
    public float delay;
    public bool singleTarget;
    public GameObject bullet;
}
