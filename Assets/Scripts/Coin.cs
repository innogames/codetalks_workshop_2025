using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Timer timer;

    private void Awake()
    {
        this.timer ??= FindFirstObjectByType<Timer>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the coin hit an enemy
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            this.timer.IncreaseCoinCount();
            // Destroy the enemy
            Destroy(gameObject);
            
            // Optional: Add particle effect or sound here
        }
    }
}