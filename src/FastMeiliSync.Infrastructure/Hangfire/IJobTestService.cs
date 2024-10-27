namespace FastMeiliSync.Infrastructure.Hangfire;

public interface IJobsService
{
    void FireAndForgetJob();
    void ReccuringJob();
    void DelayedJob();
    void ContinuationJob();
}
