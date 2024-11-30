using UnityEngine;
using Unity.Cinemachine;
using TMPro;
using UnityEditor;

namespace NattyStuff
{
    public class CameraTrigger : MonoBehaviour
    {
        public CinemachineVirtualCamera targetCamera;
        public CinemachineVirtualCamera mainCamera;
        private CinemachineVirtualCamera _currentActiveCamera;
        public bool hideAtStart = true;


        private void OnEnable()
        {
            if (mainCamera.Priority > targetCamera.Priority)
            {
                _currentActiveCamera = mainCamera;
            }
            else
            {
                _currentActiveCamera = targetCamera;
            }
        }


        private void Start()
        {
            //start main cam and hilight it in on draw gizmo
            mainCamera.Priority = 10;
            targetCamera.Priority = 0;
            _currentActiveCamera = mainCamera;

            BoxCollider boxCollider = GetComponent<BoxCollider>();

            if (boxCollider != null)
            {
                boxCollider.isTrigger = true;
            }
            else
            {
                Debug.LogError("Box Collider component not found on this GameObject.");
            }

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            TMP_Text textMeshPro = GetComponentInChildren<TMP_Text>();


            if (meshRenderer == null)
            {
                Debug.LogWarning("MeshRenderer component not found on this GameObject.");
            }

           


            if (hideAtStart)
            {

                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }

                if (textMeshPro != null)
                {
                    textMeshPro.enabled = false;
                }
            }

        }




        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                targetCamera.Priority = 10;
                mainCamera.Priority = 0;
                _currentActiveCamera = targetCamera;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                mainCamera.Priority = 10;
                targetCamera.Priority = 0;
                _currentActiveCamera = mainCamera;
            }
        }

        private void OnDrawGizmos()
        {
            if (targetCamera != null)
            {
                // Create a custom GUI style for the label
                GUIStyle labelStyle = new GUIStyle();

                // Set the base color for inactive camera labels
                labelStyle.normal.textColor = new Color(0.5f, 0.8f, 1f, 1f); // Light blue (R, G, B, A)

                // Check if the current active camera is the target camera
                if (_currentActiveCamera == targetCamera)
                {
                    // Set the color for the active camera label to green
                    labelStyle.normal.textColor = Color.green;
                }

                string labelText = "Trigger: " + gameObject.name + "\nCamera: " + targetCamera.name;
                Handles.Label(transform.position, labelText, labelStyle);

                // Reset the base color for the main camera label
                labelStyle.normal.textColor = Color.green; // Set the color for the main camera label to green

                string mainCameraLabel = "Main Camera: " + mainCamera.name;
                Handles.Label(mainCamera.transform.position, mainCameraLabel, labelStyle);
            }
        }

    }
}