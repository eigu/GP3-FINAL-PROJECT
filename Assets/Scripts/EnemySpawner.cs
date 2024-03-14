using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject EnemyPrefab2;
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
        Instantiate(EnemyPrefab, new Vector2(7.22f,3.29f),Quaternion.identity); //Spawns enemies on the gameobj spawner pos
        Instantiate(EnemyPrefab2, new Vector2(-7.53f, 3.29f),Quaternion.identity); //Spawns enemies on the gameobj spawner pos

    }
}
