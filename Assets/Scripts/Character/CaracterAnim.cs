using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterAnim : MonoBehaviour
{
    float x;
    Animator Anim;
    CharacterMove move;
    void Start()
    {
        Anim = GetComponent<Animator>();
        move = GetComponent<CharacterMove>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        Move(x);
        if (Input.GetKeyDown(KeyCode.Space) && move.isJumping)
        Jump();
    }
    void Move(float x)
    {
        Anim.SetFloat("x", Mathf.Abs(x));
    }
    void Jump()
    {
        Anim.SetTrigger("JumpTrriger");
    }
}
