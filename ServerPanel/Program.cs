using Newtonsoft.Json;
using ServerPanel.Components;
using ServerPanel.Core;

var builder = WebApplication.CreateBuilder(args);

//Load config from settings.json
{
    string jsonContent = File.ReadAllText("settings.json");
    VMConfig? vmConfig = JsonConvert.DeserializeObject<VMConfig>(jsonContent);
    if(vmConfig == null)
        return;
    Globals.AdminPassword = vmConfig.password;
    Globals.ValidToken = Globals.GenerateSecureToken(20);
    Globals.panel_title = vmConfig.panel_title;
    Console.WriteLine("Current Auth Token = " + Globals.ValidToken);
    foreach (VM vm in vmConfig.vms)
    {
        Globals.globalVms.Add(new VirtualMachine(vm.name, vm.os));
    }
}

builder.Services.AddSingleton<AuthService>();
builder.Services.AddScoped<CookieStorageAccessor>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseMiddleware<AuthMiddleware>();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();