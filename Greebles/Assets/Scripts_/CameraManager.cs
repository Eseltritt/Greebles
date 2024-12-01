using Unity.Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NattyStuff
{

    /// **Important:** Ensure that the default camera has the "DefaultCamera" tag.
    /// attach this script to an object in the scene to manage all virtual cameras
    public class CameraManager : MonoBehaviour
    {
        public List<CinemachineVirtualCamera> dynamicCameras;
        public CinemachineVirtualCamera defaultCamera;

        private void Start()
        {
            dynamicCameras = Object.FindObjectsByType<CinemachineVirtualCamera>(FindObjectsSortMode.None).ToList();

            // Sort the cameras, placing the default camera first
            dynamicCameras = dynamicCameras.OrderBy(camera => camera.tag == "DefaultCamera" ? 0 : 1).ToList();

          
        }

        public void ActivateCamera(CinemachineVirtualCamera camera)
        {
            foreach (CinemachineVirtualCamera c in dynamicCameras)
            {
                c.Priority = (c == camera) ? 10 : 0;
                Debug.Log($"Camera {c.name} priority set to {(c == camera ? "10 (active)" : "0 (inactive)")}");
            }
        }

        public void ActivateDefaultCamera()
        {
            defaultCamera.Priority = 10;
            Debug.Log($"Default camera activated: {defaultCamera.name}");
            foreach (CinemachineVirtualCamera camera in dynamicCameras)
            {
                camera.Priority = 0;
                Debug.Log($"Camera {camera.name} deactivated");
            }
        }
    }
}