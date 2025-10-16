using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CharacterMove : MonoBehaviour
{

    //public event Action<float> onRotateGun;
    //public event Action<float> onMovePerson;
    //public event Action<bool> onJumpingPerson; 
    //public event Action<bool> onSliderPerson; 

    private Rigidbody2D rigidbodyPerson;
    private Transform transformPerson;
    public Transform startPointBullet;
    public Transform gun; 

    public float speed = 5f;
    public float jumpForce = 6f;
    private float horizontal = 0f;
    private float timer = 0.5f;

    private bool isSlider = false;
    public bool isJumping = false;

    private void Awake() {
        transformPerson = transform;
        rigidbodyPerson = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Slider();
        if (isJumping)
        {
            Jumping();
            SliderToggle();
            Moving(); 
        }
        ScaleDirection();
    }
private void SliderToggle()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            timer = 0.5f;
            isSlider = true;
        }
    }
    private void Slider()
    {
        timer -= Time.deltaTime;
        isSlider = timer <= 0 ? false : true;
        //onSliderPerson.Invoke(isSlider); 
        if (isSlider)
        {
            float slideDirection = Mathf.Sign(transformPerson.localScale.x); // Get direction based on facing side
            Vector3 target = transformPerson.position + new Vector3(2 * slideDirection, 0, 0);
            transformPerson.position = Vector3.MoveTowards(transformPerson.position, target, speed * 6 * Time.deltaTime); 
        } 
    }
private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbodyPerson.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //onJumpingPerson?.Invoke(true);
            isJumping = false;
        }
        else
        {
            //onJumpingPerson?.Invoke(false);
        }
    }
private void Moving()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rigidbodyPerson.velocity = new Vector2(horizontal * speed, rigidbodyPerson.velocity.y);
        //onRotateGun?.Invoke(horizontal);
        //onMovePerson?.Invoke(horizontal);
    }
    private void ScaleDirection()
    {   
        if(horizontal < 0f)
        { 
            startPointBullet.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            transform.localScale = new Vector3(-1, 1, 0); 
        }
        else if(horizontal > 0f)
        { 
            startPointBullet.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            transform.localScale = new Vector3(1, 1, 0); 
        }
        
    }
private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Terra")
        {
            isJumping = true;  
        }
    }
}
