using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
public class GMStateJump : StateBase
{
    private Animator animator;
    public GMStateJump(Animator anim)
    {
        this.animator = anim;
    }
    public void ExecuteAnimation(int animation)
    {
        animator.SetInteger("transition", animation);
    }
    
    public override void OnStateEnter(object o = null)
    {
        ExecuteAnimation(2);
    }

    public override void OnStateExit()
    {
        ExecuteAnimation(0);
    }
}
