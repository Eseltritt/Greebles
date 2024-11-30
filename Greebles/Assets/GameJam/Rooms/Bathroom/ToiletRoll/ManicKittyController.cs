using UnityEngine;

public class ManicKittyController : MonoBehaviour
{
    public Animator catAnimator;

    public int toiletPaperHitsRequired = 50;
    public int bathMatHitsRequired = 1;
    public int towelHitsRequired = 1;

    private int toiletPaperHits = 0;
    private int bathMatHits = 0;
    private int towelHits = 0;

    public float timeLimit = 20f;
    private float elapsedTime = 0f;

    public bool isCaught = false;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Check if the cat is caught by the owner (Implement owner AI logic here)
        if (isCaught)
        {
            // Lose condition
            Debug.Log("You've been caught!");
            return;
        }

        // Check task completion and time limit
        if (elapsedTime >= timeLimit)
        {
            // Time's up, check if all tasks are completed
            if (toiletPaperHits < toiletPaperHitsRequired || bathMatHits < bathMatHitsRequired || towelHits < towelHitsRequired)
            {
                // Lose condition
                Debug.Log("Time's up! You failed.");
            }
            else
            {
                // Win condition
                Debug.Log("You won!");
            }
        }

        // Check for player input (e.g., button press)
        if (Input.GetKeyDown(KeyCode.Space) && !isCaught)
        {
            if (toiletPaperHits < toiletPaperHitsRequired)
            {
                toiletPaperHits++;
                catAnimator.SetTrigger("UnravelToiletPaperHit");
                // Play sound effect or visual effect
            }
            else if (bathMatHits < bathMatHitsRequired)
            {
                bathMatHits++;
                catAnimator.SetTrigger("PullBathMatHit");
                // Play sound effect or visual effect
            }
            else if (towelHits < towelHitsRequired)
            {
                towelHits++;
                catAnimator.SetTrigger("PullTowelHit");
                // Play sound effect or visual effect
            }
        }
    }
}