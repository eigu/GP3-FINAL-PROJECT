using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Drops : MonoBehaviour
{

    //compressed version for visualization
    //will update after spawn enemy logic after map
    //drop rates (instances) not here yet ; will also be in spawn logic

    public float bounceHeight = 0.5f;   
    public float bounceDuration = 0.5f; 
    public int bounceCount = 3;         

    private Vector3 originalPosition;
    private bool check;


    void Start()
    {
        gameObject.SetActive(true);
        check = false;
    }


    private void FixedUpdate()
    {
        
        if (transform.parent == null && !check)
        {
            gameObject.SetActive(true);
            check = true;
            originalPosition = transform.position;
            StartCoroutine(Bounce());
        }
    }

  
    IEnumerator Bounce()
    {
        for (int i = 0; i < bounceCount; i++)
        {

            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / bounceDuration;
                transform.Translate(Vector3.up * bounceHeight * Time.deltaTime / bounceDuration);
                yield return null;
            }

    
            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / bounceDuration;
                transform.Translate(Vector3.down * bounceHeight * Time.deltaTime / bounceDuration);
                yield return null;
            }
        }

        transform.position = originalPosition;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // faux pickup
        // Debug.Log("Collider entered: " + other.tag);
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }


    }
}
