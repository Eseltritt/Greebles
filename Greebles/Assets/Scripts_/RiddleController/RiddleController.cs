using UnityEngine;
using System.Collections.Generic;
using System;

public class RiddleController : MonoBehaviour
{
    [SerializeField] GameObject secretObject;
    [SerializeField] List<GameObject> riddleComponents = new List<GameObject>();
    public bool solved = false;
    public int solvedCount = 0;
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
        solvedCount = 0;
        foreach (GameObject component in riddleComponents)
        {
            if (component.activeSelf)
            {
                solved = false;                
            } else
            {
                solvedCount++;
                if(solvedCount >= riddleComponents.Count)
                {
                    solved = true;
                    Debug.Log("Riddle solved");
                    RevealSecret();
                    timerActive = false;
                    return;
                }
            }
            if (!component.activeSelf && timerEnabled && !timerActive)
            {
                RiddleTimerStart();
                Debug.Log("Timer started");
            }
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
        }
        timer = timerMax;
    }

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

    private void ResetRiddle()
    {
        Debug.Log("Riddle reset");
        foreach (GameObject component in riddleComponents)
        {
            component.SetActive(true);

        }
    }
}
