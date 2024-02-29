using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float TimerRemaining = 60; // Corrected variable name
    public bool TimeIsRunning = true; // Corrected variable name
    public TMP_Text timeText;

    void Start()
    {
        TimeIsRunning = true;
    }

    void Update()
    {
        if (TimeIsRunning)
        {
            if (TimerRemaining > 0) // Changed condition to check if timer is greater than 0
            {
                TimerRemaining -= Time.deltaTime; // Corrected decrement
                DisplayTime(TimerRemaining);
            }
            else
            {
                TimeIsRunning = false; // Stop the timer when it reaches 0
                TimerRemaining = 0; // Ensure the timer doesn't go negative
                DisplayTime(TimerRemaining);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Corrected formatting
    }
}