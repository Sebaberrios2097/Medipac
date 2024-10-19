using Medipac.Context;
using Medipac.Framework;
using Medipac.Models;
using Medipac.ReadOnly;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar dependencias personalizadas
builder.Services.AddTransient<IEmailSender, FakeEmailSender>();
DependencyRegistration.RegisterDependencies(builder.Services);

// Configurar DbContext con la cadena de conexión
builder.Services.AddDbContext<DbMedipac>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MediappCon"));
});

// Configurar Identity
builder.Services.AddIdentity<ComUsuario, IdentityRole<int>>()
    .AddEntityFrameworkStores<DbMedipac>()
    .AddDefaultTokenProviders();

// Agregar Razor Pages (Necesario para Identity)
builder.Services.AddRazorPages();  // << Agregar esta línea

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Asegúrate de que esto esté antes de UseAuthorization
app.UseAuthorization();

// Mapeo de rutas para las páginas Razor (incluyendo Identity)
app.MapRazorPages(); // << Esto habilita las Razor Pages

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
