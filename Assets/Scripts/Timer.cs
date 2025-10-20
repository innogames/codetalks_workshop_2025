using System;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private int coinGoal;
    
    private float passedTime;
    
    public float PassedTime => this.passedTime;

    private int coinsCollected;

    private bool timerIsStopped = false;
    
    private void FixedUpdate()
    {
        if (this.timerIsStopped)
            return;
        
        this.passedTime += Time.fixedDeltaTime;
    }

    public void IncreaseCoinCount()
    {
        this.coinsCollected++;
        if (this.coinsCollected >= this.coinGoal)
        {
            this.timerIsStopped = true;
        }
    }
}
