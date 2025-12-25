using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAttack : MonoBehaviour
{
    bool isCollision = false;
    CharHp charHp;
    bool isAttack;
    
    private Transform trPerson;
    Transform trEnemy;
 public float minDistanceAttack = 3;
    public event Action<bool> onAttackAnim;
    float timer;
    public float interval = 0.5f;
    public float damage = 5;
 private void Start()
 {
        trEnemy = transform;
        trPerson = FindObjectOfType<CharacterMove>().transform;
        charHp = trPerson.GetComponent<CharHp>();

 }

 void Update()
 {
     float dist = Vector2.Distance(trPerson.position, trEnemy.position);
        IsMinDistance(dist);
        if (isAttack && timer < Time.time && isCollision)
        {
            timer = Time.time + interval;
            charHp.TakeDamage(damage);
        }
 }
 public void IsMinDistance(float dist)
 {
     isAttack = minDistanceAttack >= dist;
        onAttackAnim?.Invoke(isAttack); 
 }
 void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollision = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollision = false;
        }
    }
}
