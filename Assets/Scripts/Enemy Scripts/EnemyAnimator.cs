 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Walk(bool walk)
    {
        animator.SetBool(AnimationTags.WALK_PARAM, walk);
    }
    public void Run(bool run)
    {
        animator.SetBool(AnimationTags.RUN_PARAM, run);
    }
    public void Attack()
    {
        animator.SetTrigger(AnimationTags.ATTACK_TRIG);
    }
    public void Dead()
    {
        animator.SetTrigger(AnimationTags.DEAD_TRIG);
    }
}
