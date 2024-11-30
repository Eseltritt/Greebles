using UnityEditor;
using UnityEngine;

namespace NattyStuff
{
    [CustomEditor(typeof(DeskController))]
    public class DeskControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DeskController deskController = (DeskController)target;

            if (deskController.drawerAnimators != null)
            {
                for (int i = 0; i < deskController.drawerAnimators.Length; i++)
                {
                    if (GUILayout.Button($"OpenDraw {i + 1}"))
                    {
                        // Pass the correct index, starting from 0
                        deskController.StartAnimation(i);
                    }
                }
            }
        }
    }
}
