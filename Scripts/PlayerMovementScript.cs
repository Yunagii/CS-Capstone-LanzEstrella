using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovementScript : MonoBehaviour
{
    [Header ("Unity Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] TrailRenderer tr;

    [Header ("Input Actions")]
    private InputAction move;
    private InputAction dash;
    private InputAction attack;

    [Header ("Other Scripts")]
    private PlayerInputController playerInput; 
    private PlayerAttackScript playerAttack;

    [Header ("Movement Variables")]
    public Vector2 moveInput = Vector2.zero;
    public Vector2 lastInput = Vector2.zero;
    [SerializeField] private float moveSpeed = 10.0f;

    [Header ("Dash Variables")]
    private bool isDashing = false;
    private bool canDash = true;
    private Vector2 dashDirection = Vector2.zero;
    [SerializeField] private float dashMultiplier = 3.0f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 0.5f;

    [Header ("Attack Variables")]
    private bool isAttacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
        playerInput = new PlayerInputController();
        playerAttack = GetComponent<PlayerAttackScript>();
    }

    private void OnEnable()
    {
        move = playerInput.Player.Move;
        dash = playerInput.Player.Dash;
        attack = playerInput.Player.Attack;

        move.Enable();
        dash.Enable();
        attack.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        dash.Disable();
        attack.Enable();
    }

    private void Update()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("Horizontal", moveInput.x);
            animator.SetFloat("Vertical", moveInput.y);
            animator.SetFloat("Speed", moveInput.sqrMagnitude);         // Move out of if statement when other idle animations are added
            lastInput = moveInput;
        }

        if(isDashing)
        {
            return;
        }

        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }

        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    public void Attack (InputAction.CallbackContext context)
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        isDashing = true;
        canDash = false;
        tr.emitting = true;

        dashDirection = moveInput.normalized;

        rb.velocity = dashDirection * moveSpeed * dashMultiplier;
        yield return new WaitForSeconds(dashDuration);

        rb.velocity = moveInput * moveSpeed;
        tr.emitting = false;
        isDashing = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        yield return new WaitForSeconds (dashCooldown);
        canDash = true;
    }  

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(playerAttack.attackTime);
        isAttacking = false;
        yield return new WaitForSeconds(playerAttack.attackSpeed);
    }
}
