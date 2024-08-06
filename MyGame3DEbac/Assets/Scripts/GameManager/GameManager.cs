using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;
public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }

    public enum CharacterAnimationState
    {
        IDLE,
        WALK,
        JUMP,
        STRAFE,
        BACKWARD
    }

    public StateMachine<GameStates> stateMachine;
    public StateMachine<CharacterAnimationState> characterAnimationState;
    public void Init()
    {
        stateMachine = new StateMachine<GameStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.INTRO,new StateBase());
        
    }

    public void InitAnimations()
    {
        characterAnimationState = new StateMachine<CharacterAnimationState>();
        characterAnimationState.Init();
        characterAnimationState.RegisterStates(CharacterAnimationState.IDLE,new StateBase());
    }
}
