using FluentScheduler;
using OrdersLibrary;
using OrdersLibrary.Models;
using Serilog;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace OrderProcessingApp.Classes;

public class JobRegistry : Registry
{
    public delegate void Report(List<Orders> list);
    /// <summary>
    /// Used to monitor new orders incoming
    /// </summary>
    public static event Report ReportNewOrders;
    public static List<Orders> Orders { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="jobName">Name of job to execute</param>
    /// <param name="interval">Seconds interval to wait</param>
    public JobRegistry(string jobName, int interval)
    {
        NonReentrantAsDefault();

        Schedule(PerformWork)
            .WithName(jobName)
            .ToRunEvery(interval)
            .Seconds();
    }
    
    public static void PerformWork()
    {
        Orders = DataOperations.GetNewOrders();
        ReportNewOrders(Orders);
        Log.Information($"Received orders in{nameof(PerformWork)}");
    }
}