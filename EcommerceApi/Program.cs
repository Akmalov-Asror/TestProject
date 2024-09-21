using EcommerceApi.V1.Commons.EcommerceContext;
using EcommerceApi.V1.Commons.ExtensionFuntions;
using EcommerceApi.V1.Commons.MIddlewares;
using EcommerceApi.V1.Commons.SwaggerConfigurations;
using EcommerceApi.V1.Commons.TokenGenerator.Interface;
using EcommerceApi.V1.Commons.TokenGenerator.Services;
using EcommerceApi.V1.Roles.Entities;
using EcommerceApi.V1.Users.Entities;
using EcommerceApi.V1.Users.Interfaces;
using EcommerceApi.V1.Users.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.GetConnectionString("DatabaseConnectionString");
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    // serialize enums as strings in api responses (e.g. Role)
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<EcommerceDbContext>(options =>
{
    options.UseSqlite(configuration);
    options.EnableSensitiveDataLogging();
});


builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddTransient<ITokenGenerator, TokenGenerator>();

builder.Services.AddServiceConfiguration();
builder.Services.AddServiceConfiguration()
    .AddSwaggerService(builder.Configuration);
builder.Services.AddIdentity<Users, Role>()
    .AddEntityFrameworkStores<EcommerceDbContext>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<EcommerceExceptionMiddleware>();
app.MapControllers();

app.Run();
