using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmailInformAPI.Scheduler;

public class Scheduler : IScheduler
{
    Timer IScheduler.onTime(DateTime date, TimerCallback callback)
    {

        // Calculate the amount of time left
        TimeSpan timeleft = date - DateTime.Now;

        // Logging back
        System.Diagnostics.Debug.WriteLine($"/* Email will be sent in {(int)timeleft.TotalSeconds} seconds");

        // Setting a trigger whenever the amount of left has run out
        return new Timer(callback, null, (int)timeleft.TotalMilliseconds, Timeout.Infinite);
    }
}
