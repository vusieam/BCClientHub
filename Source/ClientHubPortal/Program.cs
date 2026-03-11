
var builder = WebApplication.CreateBuilder(args);


#region -- database context --
builder.Services.AddSingleton<ClientHubDbContext>();
builder.Services.AddSingleton<SqlFileProvider>();
builder.Services.AddSingleton<BootstraperService>();
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

var bootstrapper = app.Services.GetRequiredService<BootstraperService>();
await bootstrapper.Migrations();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
