using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float climbSpeed;

    [Header("Input")]
    [SerializeField] private Vector2 moveInput;

    //Spawn position
    [SerializeField] private Vector2 startPosition;

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
        CheckGround();
        PlayerPosition();
    }

    private void FixedUpdate()
    {
        Run();
        Flip();
        Climb();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interactable")
        {
            OnInteractable();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Trap")
        {
            OnDeath();
        }

        if (other.gameObject.tag == "Bounce")
        {
            OnBounce();
        }
    }

    #region Input
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && gs.isGrounded)
        {
            gs.JumpAudio();
            rb.velocity += new Vector2(rb.velocity.x, jumpForce);
        }
    }
    #endregion
    #region Checks
    public void PlayerPosition()
    {
        gs.playerPosition = transform.position;
    }
    private bool CheckHasHorizontalSpeed()
    {
        gs.isMoving = (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon);
        return gs.isMoving;
    }    
    private bool CheckHasVerticalSpeed()
    {
        gs.isMovingOnLadder = (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon);
        return gs.isMovingOnLadder;
    }
    private bool CheckGround()
    {
        gs.isGrounded = feets.IsTouchingLayers(LayerMask.GetMask("Ground"));
        return gs.isGrounded;
    }
    private bool CheckLadder()
    {
        return capsule.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    #endregion
    #region Movement
    private void Run()
    {
        rb.velocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        anim.SetBool("isRunning", CheckHasHorizontalSpeed());
    }
    private void Flip()
    {
        if (CheckHasHorizontalSpeed())
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
    private void Climb()
    {
        if (CheckLadder())
        {
            Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
            rb.velocity = climbVelocity;
            rb.gravityScale = gravityTurnOff;
            anim.SetBool("isClimbing", CheckHasVerticalSpeed());
            gs.LadderAudio();
        }
        else
        {
            anim.SetBool("isClimbing", false);
            rb.gravityScale = gravityScaleAtStart;
        }
    }
    #endregion

    private void OnDeath()
    {
        transform.position = startPosition;
        rb.velocity = new Vector3();
        gs.fails += 1;
        gs.RestartTimer();
        gs.FailsCounter();
    }
    private void OnInteractable()
    {
        gs.LevelFinished();
    }
    private void OnBounce()
    {
        gs.BounceAudio();
    }
}
