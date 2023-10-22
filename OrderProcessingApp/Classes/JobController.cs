using FluentScheduler;
using Serilog;

namespace OrderProcessingApp.Classes;

public class JobController
{
    public static string JobName = "GetNewOrdersTask";
    public static void Start()
    {
        JobManager.Initialize(new JobRegistry(JobName, 10));
        Log.Information($"{JobName} started");
    }
    /// <summary>
    /// Stop job
    /// </summary>
    public static void Stop()
    {
        JobManager.RemoveJob(JobName);
        Log.Information($"{JobName} removed");
    }
}