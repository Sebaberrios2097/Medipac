namespace Medipac.ReadOnly
{
    public static class DropDownList
    {
        public static readonly Dictionary<string, string> Estado = new() { { "True", "Activo" }, { "False", "Inactivo" } };
        public static readonly Dictionary<string, string> Disponible = new() { { "True", "Disponible" }, { "False", "No Disponible" } };
    }
}
