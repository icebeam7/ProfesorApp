using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using ProfesorApp.Clases;
using System.Net.Http.Headers;
using System.Net;

namespace ProfesorApp.Servicios
{
    public class ServicioWebApi
    {
        const string WebApiURL = "establecer este valor";

        private static HttpClient Cliente = new HttpClient() { BaseAddress = new Uri(WebApiURL) };

        public static async Task<List<Alumno>> GetAlumnos()
        {
            List<Alumno> datos = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{WebApiURL}/api/Alumnos/";
            var respuesta = await Cliente.GetAsync(url);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                datos = JsonConvert.DeserializeObject<List<Alumno>>(json);

                var servicioStorage = new ServicioStorage();

                foreach (var alumno in datos)
                {
                    alumno.FotoURLSAS = servicioStorage.GetFullDownloadAlumnoURL(alumno.Id);
                }
            }

            return datos;
        }

        public static async Task<Alumno> GetAlumno(int id)
        {
            Alumno dato = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{WebApiURL}/api/Alumnos/{id}";
            var respuesta = await Cliente.GetAsync(url);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<Alumno>(json);
                dato.FotoURLSAS = new ServicioStorage().GetFullDownloadAlumnoURL(dato.Id);
            }

            return dato;
        }

        public static async Task<Alumno> AddAlumno(Alumno info)
        {
            Alumno dato = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"/api/Alumnos/";
            var jsonContent = JsonConvert.SerializeObject(info);

            var respuesta = await Cliente.PostAsync(url,
                new StringContent(jsonContent,
                Encoding.UTF8,
                "application/json"));

            if (respuesta.StatusCode == HttpStatusCode.Created)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<Alumno>(json);
            }
            return dato;
        }

        public static async Task<Alumno> UpdateAlumno(Alumno info)
        {
            Alumno dato = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"/api/Alumnos/{info.Id}";
            var jsonContent = JsonConvert.SerializeObject(info);
            var respuesta = await Cliente.PutAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"));

            //if (respuesta.StatusCode == HttpStatusCode.Created)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<Alumno>(json);
            }
            return dato;
        }

        public static async Task<bool> DeleteAlumno(int id)
        {
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"/api/Alumnos/{id}";
            var respuesta = await Cliente.DeleteAsync(url);
            return respuesta.IsSuccessStatusCode;
        }

        public static async Task<List<Tarea>> GetTareas()
        {
            List<Tarea> datos = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{WebApiURL}/api/Tareas/";
            var respuesta = await Cliente.GetAsync(url);
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                datos = JsonConvert.DeserializeObject<List<Tarea>>(json);
            }
            return datos;
        }

        public static async Task<Tarea> GetTarea(int id)
        {
            Tarea dato = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{WebApiURL}/api/Tareas/{id}";
            var respuesta = await Cliente.GetAsync(url);
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<Tarea>(json);
            }
            return dato;
        }

        public static async Task<Tarea> AddTarea(Tarea info)
        {
            Tarea dato = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"/api/Tareas/";
            var jsonContent = JsonConvert.SerializeObject(info);
            var respuesta = await Cliente.PostAsync(url, new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json"));
            //if (respuesta.StatusCode == HttpStatusCode.Created)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<Tarea>(json);
            }
            return dato;
        }

        public static async Task<Tarea> UpdateTarea(Tarea info)
        {
            Tarea dato = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"/api/Tareas/{info.Id}";
            var jsonContent = JsonConvert.SerializeObject(info);
            var respuesta = await Cliente.PutAsync(url, new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json"));
            //if (respuesta.StatusCode == HttpStatusCode.Created)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<Tarea>(json);
            }
            return dato;
        }

        public static async Task<bool> DeleteTarea(int id)
        {
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"/api/Tareas/{id}";
            var respuesta = await Cliente.DeleteAsync(url);
            return respuesta.IsSuccessStatusCode;
        }

        public static async Task<List<TareaAlumno>> GetTareaAlumnosByEval(bool evaluado)
        {
            List<TareaAlumno> datos = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{WebApiURL}/api/TareaAlumnos/GetTareaAlumnosByEval/{evaluado}";
            var respuesta = await Cliente.GetAsync(url);
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                datos = JsonConvert.DeserializeObject<List<TareaAlumno>>(json);
            }
            return datos;
        }

        public static async Task<TareaAlumno> GetTareaAlumno(int idTarea, int idAlumno)
        {
            TareaAlumno dato = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{WebApiURL}/api/TareaAlumnos/{idTarea}/{idAlumno}";
            var respuesta = await Cliente.GetAsync(url);
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<TareaAlumno>(json);
                dato.Alumno.FotoURLSAS = new ServicioStorage().GetFullDownloadAlumnoURL(idAlumno);
            }
            return dato;
        }

        public static async Task<TareaAlumno> AddTareaAlumno(TareaAlumno info)
        {
            TareaAlumno dato = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"/api/TareaAlumnos/";
            var jsonContent = JsonConvert.SerializeObject(info);
            var respuesta = await Cliente.PostAsync(url, new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json"));
            //if (respuesta.StatusCode == HttpStatusCode.Created)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<TareaAlumno>(json);
            }
            return dato;
        }

        public static async Task<TareaAlumno> UpdateTareaAlumno(TareaAlumno info)
        {
            TareaAlumno dato = null;
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"/api/TareaAlumnos/{info.IdTarea}/{info.IdAlumno}";
            var jsonContent = JsonConvert.SerializeObject(info);
            var respuesta = await Cliente.PutAsync(url, new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json"));
            //if (respuesta.StatusCode == HttpStatusCode.Created)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                dato = JsonConvert.DeserializeObject<TareaAlumno>(json);
            }
            return dato;
        }

        public static async Task<bool> DeleteTareaAlumno(int idTarea, int idAlumno)
        {
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"/api/TareaAlumnos/{idTarea}/{idAlumno}";
            var respuesta = await Cliente.DeleteAsync(url);
            return respuesta.IsSuccessStatusCode;
        }
    }
}
