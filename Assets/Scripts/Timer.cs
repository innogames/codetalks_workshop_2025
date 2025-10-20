using System;
using Mono.Cecil;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float passedTime;
    
    public float PassedTime => this.passedTime;
    
    private void FixedUpdate()
    {
        this.passedTime += Time.fixedDeltaTime;
    }
}
