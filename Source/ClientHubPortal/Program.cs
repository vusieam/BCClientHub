using ClientHubDatabase;
using ClientHubDatabase.Migrations;
using ClientHubPortal.Services;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);


#region -- database context --
builder.Services.AddSingleton<ClientHubDbContext>();
builder.Services.AddSingleton<SqlFileProvider>();
builder.Services.AddSingleton<BootstraperService>();
//builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion


#region -- repositories --
builder.Services.AddSingleton<IClientRepository, ClientRepository>();

#endregion


#region -- services --
builder.Services.AddSingleton<IClientService, ClientService>();

#endregion


builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


var boostrapper = app.Services.GetRequiredService<BootstraperService>();
await boostrapper.Migrations();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
