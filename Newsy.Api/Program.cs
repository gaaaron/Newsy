using Microsoft.AspNetCore.Mvc.Formatters;
using Newsy.Api.Infrastructure;
using Newsy.Application;
using Newsy.Infrastructure;
using System.Text.Json.Serialization;

namespace Newsy.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(x =>
        {
            x.OutputFormatters.RemoveType<StringOutputFormatter>();
        }).AddJsonOptions(x => 
        {
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddApplication()
                        .AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        await app.Services.InitializeInfrastructure();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
