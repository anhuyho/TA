using Microsoft.Extensions.Options;
using Service1.Models.Dtos;
using Service2.SERVICE.Services.Contracts;

namespace Service2.SERVICE.BackgroundTasks;

public class ReportTask : IHostedService
{
    private readonly IReportServices _reportServices;
    private readonly TimeDelay _timeDelay;

    public ReportTask(IReportServices reportServices, IOptions<TimeDelay> configure)
    {
        _reportServices = reportServices;
        _timeDelay = configure.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            var employees = await _reportServices.GetEmployeesAsync();
            _reportServices.CreateEmployeesReport(employees);
            await Task.Delay(1000 * _timeDelay.Second);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}