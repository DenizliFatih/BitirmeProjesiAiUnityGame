using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorHashes
{
    public static readonly int IsRunning = Animator.StringToHash("IsRunning");
    public static readonly int IsSkill1 = Animator.StringToHash("IsSkill1");
    public static readonly int IsSkill2 = Animator.StringToHash("IsSkill2");
    public static readonly int IsSkill3 = Animator.StringToHash("IsSkill3");
    public static readonly int IsAutoAttack = Animator.StringToHash("IsAutoAttack");
    public static readonly int IsUsingElixir = Animator.StringToHash("IsUsingElixir");
    public static readonly int IsIdle = Animator.StringToHash("IsIdle");
    public static readonly int IsDead = Animator.StringToHash("IsDead");
    public static readonly int IsTakeDamage = Animator.StringToHash("IsTakeDamage");
    public static readonly int IsDeadComplete = Animator.StringToHash("IsDeadComplete");
}
