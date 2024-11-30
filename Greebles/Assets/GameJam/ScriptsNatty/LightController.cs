using UnityEngine;

namespace NattyStuff
{
    using UnityEngine;
    using UnityEngine.Events;

    public class LightController : MonoBehaviour
    {
        /// <summary>
        /// The point light component to be controlled.
        /// </summary>
        [Tooltip("The point light component to be controlled.")]
        public Light pointLight;

        [Tooltip("The light settings asset containing the mood gradient and intensity information.")]
        public LightSettings lightSettings;

        [Tooltip("The minimum mood value.")]
        public int minMood = 1;

        [Tooltip("The maximum mood value.")]
        public int maxMood = 9;

        [Range(1, 9)]
        [Tooltip("The current mood of the character.")]
        public int currentMood = 1;

        [Tooltip("Sets the current mood of the character and updates the light's color and intensity accordingly.")]
        public void SetMood(int mood)
        {
            currentMood = Mathf.Clamp(mood, minMood, maxMood);
            UpdateLight();
            OnMoodChanged.Invoke();
        }

        [Tooltip("Event triggered when the mood changes.")]
        public UnityEvent OnMoodChanged;

        private void Start()
        {
            UpdateLight();
        }

        private void OnValidate()
        {
            UpdateLight();
        }

        /// <summary>
        /// Updates the light's color and intensity based on the current mood.
        /// 
        /// The color is determined by evaluating the mood gradient at the current mood value.
        /// The intensity is determined by the alpha channel of the color at the current mood value.
        /// </summary>
        [Tooltip("Updates the light's color and intensity based on the current mood.")]
        private void UpdateLight()
        {
            // Normalize the mood value to a range of 0 to 1
            float normalizedMood = (currentMood - minMood) / (float)(maxMood - minMood);
            Debug.Log("Normalized Mood: " + normalizedMood);

            // Get color and intensity from gradients based on normalized mood
            Color newColor = lightSettings.moodColorGradient.Evaluate(normalizedMood);
            float newIntensity = lightSettings.moodIntensityCurve.Evaluate(normalizedMood);
            Debug.Log("New Color: " + newColor + ", New Intensity: " + newIntensity);

            pointLight.color = newColor;
            pointLight.intensity = newIntensity;
        }
    }
}
