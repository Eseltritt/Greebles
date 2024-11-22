using UnityEngine;

public class RandomVisibility : MonoBehaviour
{
    public GameObject objectToToggleVisibility;
    [Range(0, 1)]
    public float probability = 0.1f; // Probability between 0 and 1

    /// <summary>
    /// Sets the visibility of the target object randomly based on the specified probability.
    /// </summary>
    /// <param name="objectToToggleVisibility">The object to toggle visibility for.</param>
    /// <param name="probability">The probability of the object being visible (0-1).</param>
    public void SetRandomVisibility()
    {
        // Generate a random value between 0 and 1
        float randomValue = Random.value;

        // Check if the random value is less than or equal to the probability threshold
        if (randomValue <= probability)
        {
            // If the condition is met, set the object's visibility to true
            objectToToggleVisibility.SetActive(true);
            Debug.Log("Object shown.");
        }
        else
        {
            // If the condition is not met, set the object's visibility to false
            objectToToggleVisibility.SetActive(false);
            Debug.Log("Object hidden.");
        }
    }
}