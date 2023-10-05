using NewsAPICore.BLL;
using Microsoft.OpenApi.Models;
using NewsAPICore.DTO.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

builder.Services.RegisterBLLDependencies(builder.Configuration);
builder.Services.Configure<NewsApiURLOptionDTO>(builder.Configuration.GetSection(nameof(NewsApiURLOptionDTO)));
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
        .WithOrigins(builder.Configuration.GetSection("AllowedApiURL").Get<string[]>())
        .WithHeaders("Authorization", "origin", "accept", "content-type")
        .WithMethods("GET")
        ;
    });
});
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NewsAPICore",
        Version = "v1",
        Description = "API for News Application",
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHsts();
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseHttpsRedirection();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("../swagger/v1/swagger.json", "NewsAPICore");
});

app.Run();
