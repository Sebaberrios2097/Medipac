using Microsoft.AspNetCore.Mvc.Rendering;

namespace Medipac.ReadOnly
{
    public static class GetFunctions
    {
        public static string CalcularDvRut(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }

        public static string FormatearRut(int rut, string dv)
        {
            string rutFormateado = rut.ToString("N0", new System.Globalization.CultureInfo("es-CL"));
            return $"{rutFormateado}-{dv}";
        }

        public static class MenuHelper
        {
            // Método para verificar si el área, controlador y acción son activos
            public static string IsActive(ViewContext viewContext, string area, string controller, string action)
            {
                string activeClass = "active";

                string currentArea = viewContext.RouteData.Values["area"]?.ToString();
                string currentController = viewContext.RouteData.Values["controller"]?.ToString();
                string currentAction = viewContext.RouteData.Values["action"]?.ToString();

                // Comparar área, controlador y acción actuales con los pasados
                if (string.Equals(currentArea, area, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase))
                {
                    return activeClass;
                }

                return string.Empty; // Si no es activo, devolver cadena vacía
            }
        }
    }
}
