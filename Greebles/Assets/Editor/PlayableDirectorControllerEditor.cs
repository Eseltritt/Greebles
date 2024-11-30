using UnityEditor;
using UnityEngine;


namespace NattyStuff
{
    [CustomEditor(typeof(PlayableDirectorController))]
    public class PlayableDirectorControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PlayableDirectorController controller = (PlayableDirectorController)target;

            if (GUILayout.Button("Start Animation"))
            {
                controller.StartAnimation();
            }

            if (GUILayout.Button("Reset Animation"))
            {
                controller.ResetAnimation();
            }
        }
    }
}