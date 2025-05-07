using TestAPI.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TestAPI.Features.Users;
using TestAPI.Features.Match;
using TestAPI.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


builder.Services.AddControllers(); // This line registers the controllers
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped(_ => new UserRepository());
builder.Services.AddScoped<MatchRepository>();
builder.Services.AddScoped(_ => new MatchRepository());
builder.Services.AddScoped<PredictionRepository>();
builder.Services.AddScoped(_=> new PredictionRepository());


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "/swagger"; // This makes Swagger the default page
    });
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapUserRoutes();
    endpoints.MapMatchRoutes();
    endpoints.MapPredictionRoutes();
});

app.Run();

