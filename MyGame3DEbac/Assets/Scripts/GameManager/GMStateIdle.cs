using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
public class GMStateIdle : StateBase
{
    private Animator animator;
    public GMStateIdle(Animator anim)
    {
        this.animator = anim;
    }
    public void ExecuteAnimation(int animation)
    {
        animator.SetInteger("transition", animation);
    }

    public override void OnStateEnter(object o = null)
    {
        ExecuteAnimation(0);
    }

}
