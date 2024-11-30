using UnityEngine;

namespace NattyStuff
{
    [CreateAssetMenu(fileName = "LightSettings", menuName = "Settings/Light Settings")]
    public class LightSettings : ScriptableObject
    {
        [Tooltip("A gradient defining the color of the light based on the character's mood.")]
        public Gradient moodColorGradient;

        [Tooltip("A gradient defining the intensity of the light based on the character's mood.")]
        public AnimationCurve moodIntensityCurve;
    }
}