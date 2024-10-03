using Microsoft.Extensions.DependencyInjection;
using Medipac.Data.Interfaces;
using Medipac.Data.Repositories;
using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Areas.CLI.Data.Repositories;
using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Areas.RES.Data.Repositories;
using Medipac.Areas.COM.Data.Interfaces;
using Medipac.Areas.COM.Data.Repositories;
using Medipac.Areas.ADM.Data.Interfaces;
using Medipac.Areas.ADM.Data.Repositories;

namespace Medipac.Framework
{
    public class DependencyRegistration
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IAdmAdminRepository, AdmAdminRepository>();
            services.AddScoped<IAdmCarruselNoticiasRepository, AdmCarruselNoticiasRepository>();
            services.AddScoped<IAdmNoticiasRepository, AdmNoticiasRepository>();
            services.AddScoped<IComUsuarioRepository, ComUsuarioRepository>();
            services.AddScoped<IExmCategoriaExamenRepository, ExmCategoriaExamenRepository>();
            services.AddScoped<ILogUsuarioRepository, LogUsuarioRepository>();
            services.AddScoped<IExmTipoExamenRepository, ExmTipoExamenRepository>();
            services.AddScoped<IResAgendaRepository, ResAgendaRepository>();
            services.AddScoped<IResCentroMedicoRepository, ResCentroMedicoRepository>();
            services.AddScoped<IResConvenioRepository, ResConvenioRepository>();
            services.AddScoped<IResEspecialidadesRepository, ResEspecialidadesRepository>();
            services.AddScoped<IResMedicoCentroMedicoRepository, ResMedicoCentroMedicoRepository>();
            services.AddScoped<IResMedicoConvenioRepository, ResMedicoConvenioRepository>();
            services.AddScoped<IResMedicoEspecialidadRepository, ResMedicoEspecialidadRepository>();
            services.AddScoped<IResPrevisionesRepository, ResPrevisionesRepository>();
            services.AddScoped<IResReservaRepository, ResReservaRepository>();
            services.AddScoped<ICliHistorialPacienteRepository, CliHistorialPacienteRepository>();
            services.AddScoped<ICliExamenesSolicitadosRepository, CliExamenesSolicitadosRepository>();
            services.AddScoped<ICliPacientesRepository, CliPacientesRepository>();
            services.AddScoped<ICliMedicoRepository, CliMedicoRepository>();
            services.AddScoped<ICliRecetaPacienteRepository, CliRecetaPacienteRepository>();
            services.AddScoped<IComEstadosUsuarioRepository, ComEstadosUsuarioRepository>();
            services.AddScoped<ICliSolicitudExamenRepository, CliSolicitudExamenRepository>();
        }
    }
}