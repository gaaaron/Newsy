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

        builder.Services.AddCors(
            options => options.AddDefaultPolicy(
                policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:5001",
        builder.Configuration["FrontendUrl"] ?? "https://localhost:5002"])
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

        builder.Services.AddControllers().AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            //c.SchemaFilter<ExampleSchemaFilter>();
        });


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
