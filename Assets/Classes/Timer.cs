using UnityEngine;

public class Timer
{
    float interval;
    float timePassed;

    public Timer(float interval)
    {
        this.interval = interval;
        timePassed = 0;
    }

    public void changeInterval(float newInterval)
    {
        interval = newInterval;
    }

    public float getInterval()
    {
        return interval;
    }

    public bool update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= interval)
        {
            timePassed = 0;
            return true;
        }
        return false;
    }
}
