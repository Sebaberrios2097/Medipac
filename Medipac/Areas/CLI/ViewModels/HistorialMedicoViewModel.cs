namespace Medipac.Areas.CLI.ViewModels
{
    public class HistorialMedicoViewModel
    {
        public int IdHistorial { get; set; }
        public string FechaHistorial { get; set; } = null!;
        public string PacienteNombre { get; set; } = null!;
        public string MedicoNombre { get; set; } = null!;
        public string? Historial { get; set; }
        public string Estado { get; set; } = null!;
    }

}
