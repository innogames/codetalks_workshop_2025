using System;
using UnityEngine;

public class Gem : MonoBehaviour
{
	public Action OnGemCollected;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<PlayerController>() != null)
		{
			Destroy(gameObject);
			OnGemCollected?.Invoke();
		}
	}
}
