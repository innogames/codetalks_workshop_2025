using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the coin hit an enemy
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            // Destroy the enemy
            Destroy(gameObject);
            
            // Optional: Add particle effect or sound here
        }    
    }
}