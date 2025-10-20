using UnityEngine;
using UnityEngine.Serialization;

public class Coin : MonoBehaviour
{
    [FormerlySerializedAs("timer")] [SerializeField] private CoinStopWatch m_coinStopWatch;

    private void Awake()
    {
        this.m_coinStopWatch ??= FindFirstObjectByType<CoinStopWatch>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the coin hit an enemy
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            this.m_coinStopWatch.IncreaseCoinCount();
            // Destroy the enemy
            Destroy(gameObject);
            
            // Optional: Add particle effect or sound here
        }
    }
}