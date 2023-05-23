using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public float airSpeed = 3f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;

    public float currentMoveSpeed
    {
        get
        {
            if(CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (_isRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return speed;
                        }
                    }
                    else
                    {
                        //AirState
                        return airSpeed;
                    }

                }
                else
                {
                    return 0;
                }
            } else
            {
                //can not move
                return 0;
            }
            
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get
        {
            return _isMoving;
        } 
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }}


    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning { 
        get 
        {
            return _isRunning;
        }
        private set 
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight { get { return _isFacingRight; }
        private set 
        {
            if(_isFacingRight != value)
            {
                _isFacingRight = value;

                transform.localScale *= new Vector2(-1, 1);
            }
        }
    }

    public bool CanMove { get
        {
            return animator.GetBool(AnimationStrings.canMove);
        } 
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); 

        if(IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        } else
        {
            IsMoving = false;
        }
        


    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            //face right
            IsFacingRight = true;
        } else if(moveInput.x < 0 && IsFacingRight)
        {
            //left 
            IsFacingRight = false;
        }
    }

    public void onRun(InputAction.CallbackContext context)
    {
        //moveInput = new Vector2(currentMoveSpeed * ,rb.velocity.y);
        if (context.started)
        {
            IsRunning = true;
        } else if (context.canceled)
        {
            IsRunning = false;

        }
    }

    public void onJump(InputAction.CallbackContext context)
    {
        //TODO check if alive
        if(context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity= new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }

    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }
}
