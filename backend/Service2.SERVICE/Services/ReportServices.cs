using System.Text.Json;
using Microsoft.Extensions.Options;
using Service1.Models.Dtos;
using Service2.SERVICE.Services.Contracts;

namespace Service2.SERVICE.Services;

public class ReportServices : IReportServices
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger<ReportServices> _logger;
    private readonly ServiceUrl _serviceUrl;

    public ReportServices(IHttpClientFactory clientFactory,
        IOptions<ServiceUrl> configure,
        ILogger<ReportServices> logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
        _serviceUrl = configure.Value;
    }

    public async Task<ICollection<EmployeeResponse>?> GetEmployeesAsync()
    {
        ICollection<EmployeeResponse>? employees = null;
        var request = new HttpRequestMessage(HttpMethod.Get, _serviceUrl.GetEmployees);
        var client = _clientFactory.CreateClient();
        try
        {
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStreamAsync();
                employees = await JsonSerializer.DeserializeAsync
                    <ICollection<EmployeeResponse>>(data);
            }
            else
            {
                _logger.LogWarning($"{response.StatusCode} - {response.Content}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
        }

        return employees;
    }

    public void CreateEmployeesReport(ICollection<EmployeeResponse>? employees)
    {
        if (employees != null)
        {
            var data = JsonSerializer.Serialize(employees);
            _logger.LogInformation(data);
        }
    }
}