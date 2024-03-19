using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSettings : MonoBehaviour
{


    public EnemyMovement
        enemyMovement;

    public Collider2D colliderToUse;

    private void FixedUpdate()
    {
         
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hi");
           enemyMovement.IsAttacked();
   

    }
}
