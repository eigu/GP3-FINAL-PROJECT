using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy_prefab;
    public Transform Enemy_spawner;
    // Start is called before the first frame update
    void Start()
    {
        Enemyspawn();

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Enemyspawn() 
    {
        Instantiate(Enemy_prefab, new Vector2(7.22f,3.29f),Quaternion.identity); //Spawns enemies on the gameobj spawner pos
    }
}
