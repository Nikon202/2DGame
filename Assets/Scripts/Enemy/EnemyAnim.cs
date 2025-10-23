using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
EnemyAttack attack;
private EnemyMove zombyMove;
    private Animator animator;

    private void Awake()
    {
        zombyMove = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
        attack = GetComponent<EnemyAttack>();
    }
    private void OnEnable()
    {
        zombyMove.onMoveZomby += MoveAnim;
        attack.onAttackAnim += AttackedAnim;
        zombyMove.onDeadeZomby += DeadeAnim;
    }
    private void OnDisable()
    {
        zombyMove.onMoveZomby -= MoveAnim;
        attack.onAttackAnim -= AttackedAnim;
        zombyMove.onDeadeZomby -= DeadeAnim;
    }
private void MoveAnim(float horizontal)
    {
        animator.SetFloat("x", horizontal);
    }
    private void AttackedAnim(bool isAttack)
    {
        animator.SetBool("IsAttack", isAttack);
    }
    private void DeadeAnim(bool isDeade)
    {
        animator.SetBool("DeadTr", isDeade);
    }
}
