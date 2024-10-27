using Microsoft.Extensions.Logging;

namespace FastMeiliSync.Infrastructure.Hangfire;

public class JobsService : IJobsService
{
    private ILogger _logger;

    public JobsService(ILogger<JobsService> logger)
    {
        _logger = logger;
    }

    public void ContinuationJob()
    {
        _logger.LogInformation("Hello from a Continuation job!");
    }

    public void DelayedJob()
    {
        _logger.LogInformation("Hello from a Delayed job!");
    }

    public void FireAndForgetJob()
    {
        _logger.LogInformation("Hello from a Fire and Forget job!");
    }

    public void ReccuringJob()
    {
        _logger.LogInformation("Hello from a Scheduled job!");
    }
}
