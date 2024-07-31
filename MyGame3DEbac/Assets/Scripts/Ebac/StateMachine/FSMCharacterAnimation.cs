using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
public class FSMCharacterAnimation : MonoBehaviour
{
    public enum AnimationState
    {
        IDLE,
        WALK,
        JUMP
    }

    public StateMachine<AnimationState> animationStateMachine;

    private void Start()
    {
        animationStateMachine = new StateMachine<AnimationState>();
        animationStateMachine.Init();
        animationStateMachine.RegisterStates(AnimationState.IDLE, new StateBase());
        animationStateMachine.RegisterStates(AnimationState.WALK, new StateBase());
        animationStateMachine.RegisterStates(AnimationState.JUMP, new StateBase());

    }
}
