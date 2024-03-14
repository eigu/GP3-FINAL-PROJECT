using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] int Kills;
    [SerializeField] TextMeshProUGUI KillCounter;
    [SerializeField] float AdditionalTimeMilestone = 10f;
    [SerializeField] float AdditionalTimePerKill = 5f;
    [SerializeField] float AdditionalTimerPerBossKill = 30f;


    // Start is called before the first frame update
    void Start()
    {
        remainingTime = 120; // Start with 2 minutes
        Kills = 0;
        UpdateTimerDisplay();
        UpdateKillCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

        }
        else if (remainingTime < 0) 
        {
            remainingTime = 0;
            timerText.color = Color.red;
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        UpdateTimerDisplay();
    }
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Method to update the kill counter display
    void UpdateKillCounter()
    {
        KillCounter.text = Kills.ToString();

    }
    public void AddKill(GameObject enemy)
    {
        Kills++;
        remainingTime += AdditionalTimePerKill;
        // Check if the player has killed exactly 30 enemies
        if (Kills == 30)
        {
            // Add additional time to the timer
            remainingTime += AdditionalTimeMilestone;
        }
        if (enemy.CompareTag("Boss")) 
        {
            remainingTime += AdditionalTimerPerBossKill;
        }
        UpdateKillCounter();
    }
}
