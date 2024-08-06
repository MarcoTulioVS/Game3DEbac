using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
public class GMStateBackward : StateBase
{
    private Animator animator;
    public GMStateBackward(Animator anim)
    {
        this.animator = anim;
    }
    public void ExecuteAnimation(int animation)
    {
        animator.SetInteger("transition", animation);
    }

    public override void OnStateExit()
    {
        ExecuteAnimation(0);
    }
    public override void OnStateEnter(object o = null)
    {
        ExecuteAnimation(4);
    }
}
