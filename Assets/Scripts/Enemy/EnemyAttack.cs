using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAttack : MonoBehaviour
{
    private Transform trPerson;
    Transform trEnemy;
 public float minDistanceAttack = 3;
 public event Action<bool> onAttackAnim;
 private void Start()
 {
        trEnemy = transform;
        trPerson = FindObjectOfType<CharacterMove>().transform;

 }

 void Update()
 {
     float dist = Vector2.Distance(trPerson.position, trEnemy.position);
     IsMinDistance(dist);
 }
 public void IsMinDistance(float dist)
 {
     bool isAttack = minDistanceAttack >= dist;
     onAttackAnim?.Invoke(isAttack); 
 }
}
