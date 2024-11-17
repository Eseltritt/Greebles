using UnityEngine;
using System.Collections.Generic;
using System;


// RiddleController is a script that manages the riddles in the game. It keeps track of the riddle components and the secret object that is revealed when the riddle is solved.
public class RiddleController : MonoBehaviour
{
    [SerializeField] GameObject secretObject;
    public bool solved = false;
    public int solvedCount = 0;
    public int solvedCountMax = 3;
    public bool timerEnabled = false;
    public bool timerActive = false;
    public float timer;
    public float timerMax = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideSecret();
    }

    // Update is called once per frame
    void Update()
    {        
        if (timerEnabled && timerActive)
        {
            RiddleTimerUpdate();
        }
        CheckRiddle();        
    }

    // Check if all riddle components are active
    public void CheckRiddle()
    {
        if (solved)
        {
            return;
        }
        else
        {
            HideSecret();
        }     
    }

    public void CountUp()
    {
        RiddleTimerStart();
        solvedCount++;
        Debug.Log("Riddle count: " + solvedCount);
        if (solvedCount >= solvedCountMax)
        {
            solved = true;
            Debug.Log("Riddle solved");
            RevealSecret();
            timerActive = false;
            return;
        }
    }

    // Reveal the secret object/reward for solving the riddle
    public void RevealSecret()
    {
        secretObject.SetActive(true);
    }

    // Hide the secret object/reward for solving the riddle
    public void HideSecret()
    {
        secretObject.SetActive(false);
    }

    // Timer for riddle. When timer runs out, riddle is reset.
    private void RiddleTimerStart()
    {
        if (!timerActive)
        {
            timerActive = true;
            timer = timerMax;
        }
        
    }
    //Updates the timer and checks if it is up. If it is, the riddle is reset.
    private void RiddleTimerUpdate()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;
            //Debug.Log("Timer: " + timer);
            if (timer <= 0)
            {
                ResetRiddle();
                timerActive = false;
                Debug.Log("Timer ended");
            }
        }
    }

    //Resets the riddle
    private void ResetRiddle()
    {
        solvedCount = 0;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
        Debug.Log("Riddle reset");
    }
}
