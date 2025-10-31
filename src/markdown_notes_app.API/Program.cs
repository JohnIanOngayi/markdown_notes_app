using markdown_notes_app.API.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

        // Add services to the container.
        builder.Services.ConfigureLoggerService();
        builder.Services.ConfigureCORS();
        builder.Services.ConfigureIISIntegrations();

        builder.Services.ConfigureMySQLContext(builder.Configuration);

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseHsts();

        app.UseHttpsRedirection();
        //app.UseStaticFiles();
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });
        app.UseCors("CORSPolicy");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}