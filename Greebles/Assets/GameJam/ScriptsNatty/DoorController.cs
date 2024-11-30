using UnityEngine;

namespace NattyStuff
{
    public class DoorController : MonoBehaviour


    {
        public bool isDoorOpen;

        public void OpenDoor()
        {
            isDoorOpen = true;
        }

        public void CloseDoor()
        {
            isDoorOpen = false;
        }
    }
}
