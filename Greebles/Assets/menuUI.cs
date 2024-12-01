using UnityEngine;

public class menuUI : MonoBehaviour
{
    void OnEnable()
    {
        Time.timeScale = 0f; // Pause the game
    }

    void OnDisable()
    {
        Time.timeScale = 1f; // Resume the game
    }
}