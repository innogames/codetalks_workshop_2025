using UnityEngine;

namespace DefaultNamespace
{
	public class GemStopWatch : MonoBehaviour
	{
		[SerializeField]
		private TMPro.TextMeshProUGUI timerUI;
		
		private float passedTime = 0f;
		private int collectedGems = 0;

		void Start()
		{
			foreach (Transform child in transform)
			{
				if (child.GetComponent<Gem>() != null)
				{
					child.GetComponent<Gem>().OnGemCollected += OnCollectGem;
				}
			}
		}

		void FixedUpdate()
		{
			if (collectedGems >= 5)
			{
				// timer is stopped 
			}
			else
			{
				passedTime += Time.fixedDeltaTime;
				timerUI.text = "Timer: " + passedTime.ToString("F2");
			}
		}

		private void OnCollectGem()
		{
			collectedGems++;
		}
	}
}