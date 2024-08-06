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
    public float speedHorizontal;
    public float speedVertical;

    public enum AnimationState
    {
        IDLE,
        WALK,
        JUMP,
        STRAFE,
        BACKWARD
    }

    public StateMachine<AnimationState> animationStateMachine;

    private void Start()
    {
        
        animationStateMachine = new StateMachine<AnimationState>();
        animationStateMachine.Init();
        animationStateMachine.RegisterStates(AnimationState.IDLE, new GMStateIdle(animator));
        animationStateMachine.RegisterStates(AnimationState.WALK, new GMStateWalk(animator));
        animationStateMachine.RegisterStates(AnimationState.JUMP, new GMStateJump(animator));
        animationStateMachine.RegisterStates(AnimationState.STRAFE, new GMStateStrafe(animator));

    }

    private void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        Move();
        Jump();
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX * speedHorizontal,0,moveZ * speedVertical);

        rb.AddForce(movement);

        if (movement.z > 0)
        {
            animationStateMachine.SwitchState(AnimationState.WALK);
        }
        else if(movement.z == 0)
        {
            animationStateMachine.SwitchState(AnimationState.IDLE);
        }

        if(movement.x > 0 || movement.x < 0)
        {
            animationStateMachine.SwitchState(AnimationState.STRAFE);
        }

    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
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
