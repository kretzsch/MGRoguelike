using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float endTime;
    private float startTime;
    private bool isRunning;

    public float ElapsedTime
    {
        get { return Time.time - startTime; }
    }

    void Start()
    {
        // Start the timer as soon as you enter the scene
        StartTimer();
    }

    void OnDestroy()
    {
        // Stop the timer when you exit the scene
        StopTimer();
        endTime = ElapsedTime;
    }

    public void StartTimer()
    {
        isRunning = true;
        startTime = Time.time;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}

