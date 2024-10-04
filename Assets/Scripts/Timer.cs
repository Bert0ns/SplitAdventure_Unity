using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action onTimerEnd;

    private float duration = 0f;
    private bool isTimerRunning = false;
    private float timeRemaining = 0f;

    private void Update()
    {
        if(isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            if(timeRemaining < 0f)
            {
                StopTimer();
                onTimerEnd?.Invoke();
            }
        }
    }

    public void StartTimer()
    {
        if (isTimerRunning)
        {
            throw new Exception(message: "Timer is already running but someoune tried to start it");
        }
        timeRemaining = duration;
        isTimerRunning = true;
    }
    public void PauseTimer()
    {
        isTimerRunning = false;
    }
    public void StopTimer()
    {
        timeRemaining = 0f;
        isTimerRunning = false;
    }
    public void SetDuration(float duration)
    {
        this.duration = duration;
    }
    public float GetDuration()
    {
        return this.duration;
    }
    public bool IsTimerRunning()
    {
        return isTimerRunning;
    }
}
