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
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
});

// Configurar Identity
builder.Services.AddIdentity<ComUsuario, IdentityRole<int>>()
    .AddEntityFrameworkStores<DbMedipac>()
    .AddDefaultTokenProviders();

// Agregar Razor Pages (Necesario para Identity)
builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/AccessDenied"; // Redirige a la página de Acceso Denegado
});

var app = builder.Build();

// Llamar a la función para inicializar roles y el usuario administrador
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await InitializeRolesAndAdminUser(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar los roles y el usuario administrador.");
    }
}

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
app.MapRazorPages();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task InitializeRolesAndAdminUser(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ComUsuario>>();

    string adminRole = "Administrador";
    string medicoRole = "Medico";
    string pacienteRole = "Paciente";

    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole<int>(adminRole));
    }
    if (!await roleManager.RoleExistsAsync(medicoRole))
    {
        await roleManager.CreateAsync(new IdentityRole<int>(medicoRole));
    }
    if (!await roleManager.RoleExistsAsync(pacienteRole))
    {
        await roleManager.CreateAsync(new IdentityRole<int>(pacienteRole));
    }

    // Crea un usuario administrador predeterminado
    string adminUserName = "MediAdmin";
    string adminEmail = "mediadmin@gmail.com";
    string adminPassword = "Admin2097#";  // Usa una contraseña segura en producción

    // Verifica si el usuario ya existe
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        // Crear usuario si no existe
        adminUser = new ComUsuario
        {
            UserName = adminUserName,
            Email = adminEmail,
            EmailConfirmed = true,
            FechaCreacion = DateTime.Now,
            IdEstado = 1
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            // Asignar rol de administrador
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }
    }
    else
    {
        // Asegurarse de que el usuario tenga el rol de administrador
        if (!await userManager.IsInRoleAsync(adminUser, adminRole))
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }
    }
}
