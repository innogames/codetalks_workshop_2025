using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CoinStopWatchUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CoinStopWatch m_coinStopWatch;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.timerText.text = $"{this.m_coinStopWatch.PassedTime:N0}s";
    }
}
