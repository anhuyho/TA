using Serilog;
using Service1.Models.Dtos;
using Service2.SERVICE.BackgroundTasks;
using Service2.SERVICE.Configuration;
using Service2.SERVICE.Services;
using Service2.SERVICE.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient();
    builder.Services.AddSingleton<IReportServices, ReportServices>();
    builder.Services.Configure<ServiceUrl>(builder.Configuration.GetSection("ServiceUrl"));
    builder.Services.Configure<TimeDelay>(builder.Configuration.GetSection("TimeDelay"));
    builder.Services.AddHostedService<ReportTask>();
    builder.Host.UseSerilog(SeriLoggerConfiguration.Configure);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}