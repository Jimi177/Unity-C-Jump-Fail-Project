using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float climbSpeed;

    [Header("Input")]
    [SerializeField] Vector2 moveInput;

    //Spawn position
    [SerializeField] Vector2 startPosition;

    private float gravityScaleAtStart;
    private float gravityTurnOff = 0;

    //Acces
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D capsule;
    BoxCollider2D feets;
    GameSesion gs;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider2D>();
        feets = GetComponent<BoxCollider2D>();
        gs = FindObjectOfType<GameSesion>();
        gravityScaleAtStart = rb.gravityScale;
        startPosition = transform.position;
    }

    private void Update()
    {
        AlwayesCheck();
        PlayerPosition();
    }

    private void FixedUpdate()
    {
        Run();
        Flip();
        Climb();
    }


    #region Input
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && CheckGround())
        {
            gs.JumpAudio();
            rb.velocity += new Vector2(rb.velocity.x, jumpForce);
        }
    }
    #endregion
    #region Checks
    public void PlayerPosition()
    {
        Vector2 playerPosition = transform.position;
        gs.playerPosition = playerPosition;
    }

    private bool CheckHasHorizontalSpeed()
    {
        if(Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            gs.isMoving = true;
            return true;
        }
        else
        {
            gs.isMoving = false;
            return false;
        }
    }    
    private bool ChechHasVerticalSpeed()
    {
        if (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon)
        {
            gs.isMovingOnLadder = true;
            return true;
        }
        else
        {
            gs.isMovingOnLadder = false;
            return false;
        }
    }
    private bool CheckGround()
    {
        if (feets.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            gs.isGrounded = true;
            return true;
        }
        else
        {
            gs.isGrounded = false;
            return false;
        }
    }
    private bool CheckLadder()
    {
        if (capsule.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckTrap()
    {
        if (capsule.IsTouchingLayers(LayerMask.GetMask("Trap")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckBounce()
    {
        if (capsule.IsTouchingLayers(LayerMask.GetMask("Bouncing")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckInteractable()
    {
        if (capsule.IsTouchingLayers(LayerMask.GetMask("Interactable")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void AlwayesCheck()
    {
        if (CheckTrap())
        {
            transform.position = startPosition;
            gs.fails += 1;
            gs.RestartTimer();
            gs.FailsCounter();
        }

        if (CheckInteractable())
        {
            gs.LevelFinished();
        }

        if (CheckBounce())
        {
            gs.BounceAudio();
        }

        CheckGround();
    }
    #endregion
    #region Movement
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        anim.SetBool("isRunning", CheckHasHorizontalSpeed());
    }
    void Flip()
    {
        if (CheckHasHorizontalSpeed())
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
    void Climb()
    {
        if (CheckLadder())
        {
            Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
            rb.velocity = climbVelocity;
            rb.gravityScale = gravityTurnOff;
            anim.SetBool("isClimbing", ChechHasVerticalSpeed());
            gs.LadderAudio();
        }
        else
        {
            anim.SetBool("isClimbing", false);
            rb.gravityScale = gravityScaleAtStart;
        }
    }
    #endregion
}
