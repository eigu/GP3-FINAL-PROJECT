using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator m_anim;

    private bool isAttacking = false;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Keyboard.current.jKey.isPressed)
        {
            Attacking();
        }
        else
        {
            NotAttacking();
        }
    }

    // move this to an animation controller -jay
    // only implement hp modifiier logic here (ie: damage)
    #region AnimationLogic
    private void Attacking()
    {
        isAttacking = true;
        m_anim.SetBool("IsAttacking", isAttacking);
    }

    private void NotAttacking()
    {
        isAttacking = false;
        m_anim.SetBool("IsAttacking", isAttacking);
    }
    #endregion
}
