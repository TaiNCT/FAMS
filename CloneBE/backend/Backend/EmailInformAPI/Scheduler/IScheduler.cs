namespace EmailInformAPI.Scheduler
{
    public interface IScheduler
    {
        public Timer onTime(DateTime date, TimerCallback callback);
    }
}
