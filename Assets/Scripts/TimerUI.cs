using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Timer timer;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.timerText.text = $"{this.timer.PassedTime:N0}s";
    }
}
