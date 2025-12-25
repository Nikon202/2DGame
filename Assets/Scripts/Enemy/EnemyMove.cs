using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMove : MonoBehaviour
{
    EnemyHp hp;
    public event Action<float> onMoveZomby;
    [SerializeField] private Transform trPerson;
    private Transform trZomby;
    private Rigidbody2D rbZomby;
    private Collider2D col;

    public float speed = 5f;
    public float scale = 1f;
    public float jumpForce = 6f;
    private float moveInput = 1f;
    private Vector3 currentPoint;

    private bool isJumping = false; 
    private bool isJumpWait = false;
    private bool isFollow = false;
    bool isStop = false;

    private float timer = 4f;
    private float distance = 0f;
    void Awake()
        {
            rbZomby = GetComponent<Rigidbody2D>();
            trZomby = GetComponent<Transform>();
            hp = GetComponent<EnemyHp>();
            col = GetComponent<Collider2D>();
        }
    void OnEnable()
        {
            hp.onDeadeZomby += StopMove;
        }
        private void Start()
        {
        trPerson = FindObjectOfType<CharacterMove>().transform;
        trZomby.position = new Vector3(scale, scale, scale);
        StartCoroutine(JumpingCoroutine());
    }
    private void OnDisable()
    {
        StopCoroutine(JumpingCoroutine());
        hp.onDeadeZomby -= StopMove;
    }

    void Update()
    {
        if (isStop) return;
        CheckDistance();
        if (distance <= 15f)
        {
            Follow();
            isFollow = true;
        }
        else
        {
            isFollow = false;
        }
        Moving();
        if (isJumping && isJumpWait) Jumping();
        ScaleDirection();
    }
    void StopMove()
    {
        rbZomby.isKinematic = true;
        col.enabled = false;
        rbZomby.velocity = Vector3.zero;
        isStop = true;
    }
    private void CheckDistance()
        {
            distance = Vector2.Distance(trZomby.position, trPerson.position);
        }
    private void Follow()
    {
        Vector2 direction = trPerson.position - trZomby.position;
        moveInput = direction.x > 0 ? 1 : -1;
    }
    private void Moving()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !isFollow)
        {

            timer = UnityEngine.Random.Range(3, 6);
            moveInput = UnityEngine.Random.Range(-1, 1);
            moveInput = moveInput < 0 ? 1 : -1;
        }
        rbZomby.velocity = new Vector2(moveInput * speed, rbZomby.velocity.y);
        onMoveZomby?.Invoke(Mathf.Abs(moveInput));
    }
    private void Jumping()
        {
            rbZomby.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
            isJumpWait = false; 
        }

    private IEnumerator JumpingCoroutine()
    {
        while (true)
        {
            currentPoint = trZomby.position;
            yield return new WaitForSeconds(0.1f);
            if (currentPoint == trZomby.position)
            {
                isJumpWait = true;
            }
        }
    }
    private void ScaleDirection()
        {
            if (moveInput < 0f)
            {
                transform.localScale = new Vector3(-scale,scale, scale);
            }
            else if (moveInput > 0f)
            {
                transform.localScale = new Vector3(scale,scale, scale);
            }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Terra")
        {
            isJumping = true;
        }
    }
}