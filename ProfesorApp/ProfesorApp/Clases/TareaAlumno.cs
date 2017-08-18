using System;

namespace ProfesorApp.Clases
{
    public class TareaAlumno
    {
        public int IdTarea { get; set; }
        public int IdAlumno { get; set; }

        public string Mensaje { get; set; }
        public string ArchivoURL { get; set; }
        public DateTime Fecha { get; set; }
        public int Calificacion { get; set; }
        public bool Evaluado { get; set; }

        public Tarea Tarea { get; set; }
        public Alumno Alumno { get; set; }

        public string FechaRespuestaDate { get { return Fecha.ToString("dd/MM/yyyy"); } }
        public string EvaluadoString { get { return Evaluado ? "Tarea evaluada" : "Pendiente de evaluar"; } }
        public string MensajeCorto { get { return (Mensaje.Length > 50) ? Mensaje.Substring(0, 50) : Mensaje; } }
    }
}
