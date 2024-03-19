using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    private Animator m_anim;
    [SerializeField] private Rigidbody2D m_rigidBody;
    [SerializeField] private Transform playerPos;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private Vector2 m_movementInput;
    public EnemyMovement attackInd;
    public AttackPatterns immuneInd;
    public Combat combat;
    public Player playerEn;
    public Enemy enemyEn;

    


    public bool isAttacking = false;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _sprintMultiplier;
    [SerializeField] private float jumpBackSpeed;

    private Color dimColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    private Vector2 m_lastMovementDirection = Vector2.zero; // for storing last direction faced when walking stops
    private bool canPass = true; 

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        
       bool attacked = attackInd.attacked;
        bool immune = immuneInd.immune;

        if (attacked && !immune)
        {
            IsAttacked();
        }


       else if (!attacked)
        {
            ResetSpriteColor
                ();
        }

        #region AnimationLogic
        m_anim.SetFloat("Horizontal", m_movementInput.x);
        m_anim.SetFloat("Vertical", m_movementInput.y);
        m_anim.SetFloat("Speed", m_movementInput.sqrMagnitude);
        if (m_movementInput.magnitude > 0.1f)
        {
            m_lastMovementDirection = m_movementInput.normalized;
        }
        else
        {
            m_anim.SetFloat("Horizontal", m_lastMovementDirection.x);
            m_anim.SetFloat("Vertical", m_lastMovementDirection.y);
        }
        #endregion

        float currentMoveSpeed = _movementSpeed;

        if (Keyboard.current.leftShiftKey.isPressed) currentMoveSpeed *= _sprintMultiplier;

        if (m_movementInput == Vector2.zero) m_rigidBody.velocity = Vector2.zero;

        m_rigidBody.velocity = m_movementInput.normalized * currentMoveSpeed;
        
    }



    private void OnCollisionEnter(Collision collision) //is trigger allows player to pass thru walls, this is for that
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            
            StopPlayer();
        }
    }

    private void StopPlayer()
    {
        canPass = false;
    }

    private void OnMove(InputValue inputValue)
    {
        m_movementInput = inputValue.Get<Vector2>();
    }

    private void IsAttacked()
    {
        m_anim.SetBool("IsAttacking", false);
        Vector2 backwardDirection = -transform.right;
        Debug.Log("hello");
        transform.position += (Vector3)backwardDirection * jumpBackSpeed * Time.deltaTime;
        DimSprite();
        DealDamageToPlayer();
    }

    private void DimSprite()
    {
     spriteRenderer.color = dimColor;
    }

    private void ResetSpriteColor()
    {
        spriteRenderer.color = Color.white;
    }

    private void DealDamageToPlayer()
    {
        Debug.Log("ermm");
        combat.DealDamage(enemyEn, playerEn);
    }
}
