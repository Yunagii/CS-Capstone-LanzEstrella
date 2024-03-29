using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttackScript : MonoBehaviour
{
    [Header ("Input Actions")]
    private InputAction attack;

    [Header ("Other Scripts")]
    private PlayerMovementScript playerMovement;
    private PlayerInputController playerInput;

    [Header ("Attack Variables")]
    private GameObject attackArea = default;
    private bool isAttacking = false;
    private bool canAttack = true;
    public float attackTime = 0.1f;
    public float attackSpeed = 0.4f;

    [Header ("Movement Variables")]
    private bool movingWest = false;
    private bool movingEast = false;
    private bool movingNorth = false;
    private bool movingSouth = false;
    private bool movingNW = false;
    private bool movingNE = false;
    private bool movingSW = false;
    private bool movingSE = false;

    private void Awake()
    {
        playerInput = new PlayerInputController();
        playerMovement = gameObject.GetComponent<PlayerMovementScript>();
        attackArea = transform.GetChild(0).gameObject;
        attackArea.SetActive(isAttacking);
        
    }

    private void OnEnable()
    {
        attack = playerInput.Player.Attack;
        attack.Enable();
    } 

    private void OnDisable()
    {
        attack.Disable();
    }    

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && canAttack)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private void SetAttackDireciton()
    {
        movingWest = playerMovement.lastInput.x < 0;
        movingEast = playerMovement.lastInput.x > 0;
        movingNorth = playerMovement.lastInput.y > 0;
        movingSouth = playerMovement.lastInput.y < 0;

        movingNW = playerMovement.lastInput.x < 0 && playerMovement.lastInput.y > 0;
        movingNE = playerMovement.lastInput.x > 0 && playerMovement.lastInput.y > 0;
        movingSW = playerMovement.lastInput.x < 0 && playerMovement.lastInput.y < 0;
        movingSE = playerMovement.lastInput.x > 0 && playerMovement.lastInput.y < 0;

        if (movingWest)
        {
            attackArea.transform.localScale = new Vector3(-1f, 1f);
            attackArea.transform.localPosition = new Vector3(-0.75f, 0f);
        }

        if (movingEast)
        {
            attackArea.transform.localScale = new Vector3(1f, 1f);
            attackArea.transform.localPosition = new Vector3(0.75f, 0f);
        }

        if (movingNorth)
        {
            attackArea.transform.localScale = new Vector3(-1f, 1f);
            attackArea.transform.localPosition = new Vector3(0, 0.75f);
        }

        if (movingSouth)
        {
            attackArea.transform.localScale = new Vector3(1f, -1f);
            attackArea.transform.localPosition = new Vector3(0, -0.75f);
        }

        if (movingNW)
        {
            attackArea.transform.localScale = new Vector3(-1f, 1f);
            attackArea.transform.localPosition = new Vector3(-0.65f, 0.65f);
        }

        if (movingNE)
        {
            attackArea.transform.localScale = new Vector3(1f, 1f);
            attackArea.transform.localPosition = new Vector3(0.65f, 0.65f);
        }

        if (movingSW)
        {
            attackArea.transform.localScale = new Vector3(-1f, -1f);
            attackArea.transform.localPosition = new Vector3(-0.65f, -0.65f);
        }

        if (movingSE)
        {
            attackArea.transform.localScale = new Vector3(1f, -1f);
            attackArea.transform.localPosition = new Vector3(0.65f, -0.65f);
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        canAttack = false;

        SetAttackDireciton();

        Debug.Log("Swing");
        attackArea.SetActive(isAttacking);
        yield return new WaitForSeconds(attackTime);

        isAttacking = false;
        attackArea.SetActive(isAttacking);
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}
