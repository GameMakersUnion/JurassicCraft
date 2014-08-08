using System;

public class Timer
{

    public static long tickSize = 10000000;

    //timer stuff
    bool timing;
    private DateTime timeStart;
    private DateTime timeEnd;

    private TimeSpan _time;
    public TimeSpan time
    {
        get
        {
            if (timing)
                return DateTime.Now - timeStart;
            else
                return _time;
        }
    }

    public Timer()
    {
        timing = true;
        timeStart = DateTime.Now;
    }


    public void Stop()
    {
        if (timing)
        {
            timing = false;
            timeEnd = DateTime.Now;
            _time = timeEnd - timeStart;
        }
    }


    public string ToString()
    {

        //mandatory 3 digits of milliseconds
        //mandatory period between seconds . milliseconds
        //mandatory 1 digit of seconds, optional second.    >> {ref:#0}
        //nicely round everything higher, optionally showing colons as needed

        string output;


        if ((int)time.TotalHours > 0)
        {
            output = String.Format("{0:#}:{1:00}:{2:00}.{3:000}", time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
        }

        else if ((int)time.TotalMinutes > 0)
        {
            output = String.Format("{0:#}:{1:00}.{2:000}", time.Minutes, time.Seconds, time.Milliseconds);

        }
        else if ((int)time.TotalMilliseconds > 0)
        {
            output = String.Format("{0:0}.{1:000}", time.Seconds, time.Milliseconds);

        }

        else
        {
            output = time.ToString(); // fuck it, stop trying format
        }


        return output;

    }
}