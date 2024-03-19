using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public void DealDamage(Entity attacker, Entity defender)
    {
       
        int baseDamage = (attacker.CurrentATK) * attacker.GetDamageFactor();

        int damageTaken = baseDamage - ((defender.CurrentDEF/100) * defender.GetDefenseFactor());

        defender.TakeDamage(damageTaken);
    }
}


