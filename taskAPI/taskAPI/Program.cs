using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Notification;
using System.Reflection;
using taskAPI.DataContext;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TasksDataContext>
    (o => o.UseNpgsql(builder.Configuration.GetConnectionString("TaskDetailsDb")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((x) => true)
                   .AllowCredentials();
        });
});
builder.Services.AddSignalR();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<SignalHub>("/pushNotification");
});
app.Seed();

app.Run();
