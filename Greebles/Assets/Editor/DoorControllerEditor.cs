using UnityEditor;
using UnityEngine;


namespace NattyStuff
{
    using UnityEditor;

    [CustomEditor(typeof(DoorController))]
    public class DoorControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DoorController  myScript = (DoorController)target;
            if (GUILayout.Button("Open Door"))
            {
                myScript.OpenDoor();
            }

            if (GUILayout.Button("Close Door"))
            {
                myScript.CloseDoor();
            }
        }
    }

}