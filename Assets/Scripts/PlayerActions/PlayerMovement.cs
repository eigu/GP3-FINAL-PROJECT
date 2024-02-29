using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;
    private Vector2 m_movementInput;
    private Animator m_anim;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _sprintMultiplier;

    private Vector2 m_lastMovementDirection = Vector2.zero; // for storing last direction faced when walking stops

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // move this to an animation controller -jay
        // only implement actual movement logic here
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

    private void OnMove(InputValue inputValue)
    {
        m_movementInput = inputValue.Get<Vector2>();
    }
}
