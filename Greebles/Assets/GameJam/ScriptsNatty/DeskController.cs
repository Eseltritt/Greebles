using System.Collections;
using UnityEngine;

namespace NattyStuff
{
    public class DeskController : MonoBehaviour
    {
        public Animator[] drawerAnimators;


        public void StartAnimation(int index)
        {
            if (index >= 0 && index < drawerAnimators.Length)
            {
                Debug.Log("Starting animation for drawer " + index);
                drawerAnimators[index].Play("DrawOpen");
                StartCoroutine(WaitForAnimationAndReveal(drawerAnimators[index]));
            }
            else
            {
                Debug.LogError("Invalid drawer index: " + index);
            }
        }


        private IEnumerator WaitForAnimationAndReveal(Animator animator)
        {
            yield return new WaitForSeconds(2f);

            // Assuming the DrawerItem component is attached to the same GameObject as the Animator
            DrawerItem drawerItem = animator.GetComponent<DrawerItem>();
            if (drawerItem != null)
            {
                drawerItem.RevealItem();
            }
            else
            {
                Debug.LogError("DrawerItem component not found on Animator");
            }
        }
    }
}
