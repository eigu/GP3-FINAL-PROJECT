using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //private bool enemy;
    public Animator animator;
    public Timer timer;
    void FixedUpdate()
    {
        Debug.Log(update);
        if (update <= 0)
        {
            DetachDrops(transform);
            animator.SetBool("toAttack", false);
            Debug.Log("o yea"!);
            timer.AddKill(gameObject);
           Destroy(gameObject);
        }
    }

    void DetachDrops(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Transform child = parent.GetChild(i);
            child.SetParent(null);
        }
    }
}
