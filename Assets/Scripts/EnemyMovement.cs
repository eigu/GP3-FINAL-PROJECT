using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public Transform enemySprite;
    private Animator enemy;
    // [SerializeField] Animator m_anim;
    private bool toAttack;
    public bool attacked;
    [SerializeField] public float speed;
    [SerializeField] public float attackRange;
    [SerializeField] public float damageAmount;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField]  GameObject enemyObject; // will change for prefab
    private int playerHealth = 10; //test only

    bool waiting;
    public Enemy enemyEn;
    public Player playerEn;

    public AttackPatterns attackPatterns;
    public PlayerAttack playerAttack;

    [SerializeField] private float jumpBackSpeed;

    private Color dimColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    private bool canPass = true;

    public Combat combat;
    public Vector3 lastPosition;

    private void Awake()
    {
        enemy = GetComponent<Animator>();
        toAttack = false;
        attacked = false;
        waiting = false;

    }
    void FixedUpdate()
    {
        bool attackFromPlayer = playerAttack.attacked;
       // int enemyHP = enemyEn.CurrentHP;

        //if (player != null)
        //{
        //    MoveTowardsPlayer();
        //}

        lastPosition = transform.position;

        if (attackFromPlayer)
        {
            StartCoroutine(Wait());
            IsAttacked();
        }

        else if (!attackFromPlayer)
        {
            ResetSpriteColor
                ();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.CompareTag("Player") && toAttack)
        {
            Debug.Log("Enemy hits player!");
            attacked = true;

            if (playerHealth > 0)
            {

            }
        }

        if (other.CompareTag("Slashes"))
        {
            IsAttacked();
            Debug.Log("hit by slash");
        }


    }

    //void FindPlayer()
    //{

    //    GameObject playerObj = GameObject.FindWithTag("Player");

    //    if (playerObj != null)
    //    {
    //        player = playerObj.transform;
    //    }
    //}

    public void MoveTowardsPlayer()
    {

        Vector3 directionToPlayer = (player.position - transform.position).normalized;


        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        transform.Translate(directionToPlayer * speed * Time.deltaTime);
        if (distanceToPlayer <= attackRange && waiting == false)
        {

            toAttack = true;
            enemy.SetBool("toAttack", toAttack);
            AttackPlayer();
        }
        else
        {
            //exc 
            transform.Translate(directionToPlayer * speed * Time.deltaTime);
            toAttack = false;
            enemy.SetBool("toAttack", toAttack);
            attacked = false;
        }
    }

     void AttackPlayer()
    {


        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        Vector3 rightDirection = transform.right;


        if (angle < 135f)
        {
            enemy.SetBool("Side", true);
            enemy.SetBool("Front", false);
        }

        else
        {
            enemy.SetBool("Front", true);
            enemy.SetBool("Side", false);


        }

 
    }



    public void IsAttacked()
    {
        enemy.SetBool("toAttack", false);
        Vector2 backwardDirection = -transform.right;

        enemySprite.position += (Vector3)backwardDirection * jumpBackSpeed * Time.deltaTime;
        DimSprite();
        DealDamageToEnemy();

    }

    private void DimSprite()
    {
        spriteRenderer.color = dimColor;
    }

    IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(1f);
        waiting = false;
    }
    private void ResetSpriteColor()
    {
        spriteRenderer.color = Color.white;
    }

    private void StopEnemy()
    {
        canPass = false;


    }

  

    private void DealDamageToEnemy()
    {
        combat.DealDamage(playerEn, enemyEn);
    }

  


}
