using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreebleExplosion : MonoBehaviour
{
    public string playerTag = "Player";          // Tag to identify the player
    public GameObject explosionEffectPrefab;     // The particle system prefab for the explosion

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the player tag
        if (other.CompareTag(playerTag))
        {
            Explode();
        }
    }

    void Explode()
    {
        // Instantiate the explosion effect at the monster's position and rotation
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Destroy the monster game object
        Destroy(gameObject);
    }
}