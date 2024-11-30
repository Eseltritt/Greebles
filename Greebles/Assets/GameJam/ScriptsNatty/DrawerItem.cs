using UnityEngine;

namespace NattyStuff
{
    public class DrawerItem : MonoBehaviour
    {
        public GameObject item;
        public GameObject monster;

        [Range(1, 100)]
        public int monsterSpawnPercentage = 50; // Adjust this value to set the probability

        public static DrawerItem activeDrawer; // Static reference to the currently active drawer

        public void RevealItem()
        {
            // Deactivate items in the previously active drawer
            if (activeDrawer != null && activeDrawer != this)
            {
                activeDrawer.DeactivateItems();
            }

            // Set this drawer as the active one
            activeDrawer = this;

            Debug.Log("Revealing item in drawer");

            // Directly use the specified percentage to determine the outcome
            if (Random.value <= monsterSpawnPercentage / 100f)
            {
                Debug.Log("Spawning monster");
                monster.SetActive(true);
            }
            else
            {
                Debug.Log("Spawning item");
                item.SetActive(true);
            }
        }

        public void DeactivateItems()
        {
            item.SetActive(false);
            monster.SetActive(false);
        }
    }
}