using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
public class FSMCharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public bool isGrounded;
    public float groundCheckRadius = 0.2f; 
    public LayerMask groundLayer;

    
    public float jumpForce = 7f;

    public Transform groundCheck;
    public Rigidbody rb;


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
        animationStateMachine.RegisterStates(AnimationState.IDLE, new GMStateIdle(animator));
        animationStateMachine.RegisterStates(AnimationState.WALK, new GMStateWalk(animator));
        animationStateMachine.RegisterStates(AnimationState.JUMP, new GMStateJump(animator));

    }

    private void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        Move();
        Jump();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animationStateMachine.SwitchState(AnimationState.WALK);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {

            animationStateMachine.SwitchState(AnimationState.IDLE);
        }


    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("T");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine("PlayJump");
            
        }
        
    }

    IEnumerator PlayJump()
    {
        animationStateMachine.SwitchState(AnimationState.JUMP);
        yield return new WaitForSeconds(1.9f);
        animationStateMachine.SwitchState(AnimationState.IDLE);
    }
   
}
