using Medipac.Context;
using Medipac.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Medipac.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar dependencias personalizadas
DependencyRegistration.RegisterDependencies(builder.Services);

// Configurar DbContext con la cadena de conexión
builder.Services.AddDbContext<DbMedipac>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MediappCon"));
});

// Configurar Identity
builder.Services.AddIdentity<ComUsuario, IdentityRole<int>>()
    .AddEntityFrameworkStores<DbMedipac>()  // Utilizar el DbContext para Identity
    .AddDefaultTokenProviders();

// Configurar las opciones de Identity (opcional)
builder.Services.Configure<IdentityOptions>(options =>
{
    // Configurar reglas de password (puedes ajustar estas opciones a tu gusto)
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

    // Configurar opciones de bloqueo
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Configurar opciones de usuario
    options.User.RequireUniqueEmail = true;
});

// Configurar la cookie de autenticación
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

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

// Middleware de autenticación y autorización
app.UseAuthentication();  // Asegúrate de llamar a este antes de UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
