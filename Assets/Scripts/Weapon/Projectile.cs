using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
private Transform bulletTrans; 
        public float speed = 10f;
        public float damage = 5f;
        private float timer = 5f;
        private void Awake()
        {
            bulletTrans = GetComponent<Transform>(); 
        }
        private void Update()
        {
            timer -=Time.deltaTime;
            if(timer <= 0)
            {
                Destroy(gameObject);
            }
            bulletTrans.position += transform.right * speed * Time.deltaTime;
        }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHp>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
