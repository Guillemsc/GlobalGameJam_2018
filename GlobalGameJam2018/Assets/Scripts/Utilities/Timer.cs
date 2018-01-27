using UnityEngine;

public class Timer
{
    public void Start()
    {
        start_time = Time.timeSinceLevelLoad;
    }

    public float GetTime()
    {
        return Time.timeSinceLevelLoad - start_time;
    }

    float start_time = 0.0f;
}
