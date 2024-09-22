using IdentityApi.Extensions;
using Microsoft.Extensions.Hosting.Internal;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration).AddConsul();
builder.Services.ConfigureConsul(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build(); 
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.UseOcelot();
app.RegisterWithConsul();

app.Run();