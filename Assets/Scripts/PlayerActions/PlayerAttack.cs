using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    public bool attacked;
    public AttackPatterns attackPatterns;
    bool playerAttackStatus;
    public Animator enemy;

    public Combat combat;
    public Player playerEn;
    public Enemy enemyEn;

    private void Start()
    {
       playerAttackStatus  = attackPatterns.isAttackingOut;
    }
    void Update()
    {
        playerAttackStatus = attackPatterns.isAttackingOut;
    
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && playerAttackStatus == true)
        {
            attacked = true;
   
        }


    }
}
