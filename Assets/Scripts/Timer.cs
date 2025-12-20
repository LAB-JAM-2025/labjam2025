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
