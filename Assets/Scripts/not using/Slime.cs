using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : ScriptableObject
{
    public string enemyName;
    public int health;
    public int damage;
    public float movementSpeed;

    public void Find(GameObject target)
    {

    }
    public void Attack(GameObject target)
    {
        // Implement attack logic here
        Debug.Log($"{enemyName} attacks {target.name} for {damage} damage!");
    }

    public void DamageTaken()
    {
        
    }

    public void Death()
    {
        
    }
}
