using System;

namespace ProfesorApp.Clases
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ArchivoURL { get; set; }
        public DateTime FechaPublicacion { get; set; } = DateTime.Now;
        public DateTime FechaLimite { get; set; } = DateTime.Now;

        public string FechaPublicacionDate { get { return FechaPublicacion.ToString("dd/MM/yyyy"); } }
        public string FechaLimiteDate { get { return FechaLimite.ToString("dd/MM/yyyy"); } }
    }
}
