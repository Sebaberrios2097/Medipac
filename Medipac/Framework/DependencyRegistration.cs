using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Areas.COM.Data.Interfaces;
using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Data.ADM.Interfaces;
using Medipac.Data.Repositories;

namespace Medipac.Framework
{
    public class DependencyRegistration
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            _ = services.AddScoped<IComUsuarioRepository, ComUsuarioRepository>();
            _ = services.AddScoped<ILogUsuarioRepository, LogUsuarioRepository>();
            _ = services.AddScoped<IResAgendaRepository, ResAgendaRepository>();
            _ = services.AddScoped<IResEspecialidadesRepository, ResEspecialidadesRepository>();
            _ = services.AddScoped<IResMedicoEspecialidadRepository, ResMedicoEspecialidadRepository>();
            _ = services.AddScoped<IResReservaRepository, ResReservaRepository>();
            _ = services.AddScoped<IResHorarioMedicoRepository, ResHorarioMedicoRepository>();
            _ = services.AddScoped<ICliHistorialPacienteRepository, CliHistorialPacienteRepository>();
            _ = services.AddScoped<ICliPacientesRepository, CliPacientesRepository>();
            _ = services.AddScoped<ICliMedicoRepository, CliMedicoRepository>();
            _ = services.AddScoped<ICliRecetaPacienteRepository, CliRecetaPacienteRepository>();
            _ = services.AddScoped<IComEstadosUsuarioRepository, ComEstadosUsuarioRepository>();
            _ = services.AddScoped<IAdmNoticiasRepository, AdmNoticiasRepository>();
            _ = services.AddScoped<IAdmCarruselNoticiasRepository, AdmCarruselNoticiasRepository>();
        }
    }
}