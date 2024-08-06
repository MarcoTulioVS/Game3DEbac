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
    public float speed;
    public float deceleration = 5f;
    public bool isWalkingRight;

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
        animationStateMachine.RegisterStates(AnimationState.BACKWARD, new GMStateBackward(animator));

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

        Vector3 movement = new Vector3(moveX,0,moveZ) * speed;
        
        //Decelaration
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = movement - new Vector3(velocity.x, 0, velocity.z);
        velocityChange.y = 0;

        if(movement.x >0 || movement.x<0)
        {
            isWalkingRight = true;
            animationStateMachine.SwitchState(AnimationState.STRAFE);
            rb.AddForce(velocityChange, ForceMode.Acceleration);

        }else if (movement.x == 0)
        {
            isWalkingRight = false;
        }

        if(movement.z > 0.1f)
        {
            rb.AddForce(velocityChange,ForceMode.Acceleration);
            animationStateMachine.SwitchState(AnimationState.WALK);

        }else if (movement.z < 0 && !isWalkingRight)
        {
            animationStateMachine.SwitchState(AnimationState.BACKWARD);
            rb.AddForce(velocityChange, ForceMode.Acceleration);
        }
        else if(movement.z == 0 && !isWalkingRight)
        {
            
            animationStateMachine.SwitchState(AnimationState.IDLE);
            rb.velocity = new Vector3(Mathf.Lerp(velocity.x, 0, Time.deltaTime * deceleration),
                velocity.y,
                Mathf.Lerp(velocity.z, 0, Time.deltaTime * deceleration));
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
