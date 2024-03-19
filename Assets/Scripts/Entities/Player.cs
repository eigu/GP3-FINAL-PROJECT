using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    void FixedUpdate()
    {
        Debug.Log(update);
        if (update <= 0)
        {
            Debug.Log("o yea");
            Destroy(gameObject);
            //switch to game over screen
        }
    }
}
