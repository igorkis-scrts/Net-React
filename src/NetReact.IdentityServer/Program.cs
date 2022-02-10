using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NetReact.IdentityServer;
using NetReact.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder();

var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
var envName = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"{assemblyName}.appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"{assemblyName}.appsettings.{envName}.json", optional: true, reloadOnChange: true);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetReact.IdentityServer", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("NetReactIdentity");
builder.Services.AddDbContext<IdentityContext>(options =>
	options.UseSqlServer(connectionString,
		x => x.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

builder.Services.AddScoped<IProfileService, IdentityProfileService>();

builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>(options =>
	{
		options.Password.RequiredLength = 5;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequireUppercase = false;
		options.Password.RequireLowercase = false;
		options.Password.RequireDigit = false;
	}).AddEntityFrameworkStores<IdentityContext>()
	.AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
	{
		options.Events.RaiseErrorEvents = true;
		options.Events.RaiseInformationEvents = true;
		options.Events.RaiseFailureEvents = true;
		options.Events.RaiseSuccessEvents = true;
	})
	.AddInMemoryIdentityResources(Config.IdentityResources)
	.AddInMemoryApiResources(Config.ApiResources)
	.AddInMemoryApiScopes(Config.ApiScopes)
	.AddInMemoryClients(Config.Clients)
	.AddAspNetIdentity<ApplicationIdentityUser>()
	.AddProfileService<IdentityProfileService>();

builder.Services.AddAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetReact.IdentityServer v1"));
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetReact.IdentityServer v1"));

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

using (var scope = app.Services.CreateScope())
{
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationIdentityUser>>();
	IdentityDataSeeder.SeedAll(userManager);
}

app.Run();