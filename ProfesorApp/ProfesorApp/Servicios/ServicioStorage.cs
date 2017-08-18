using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.IO;
using System;

namespace ProfesorApp.Servicios
{
    public class ServicioStorage
    {
        const string StorageURL = "este valor lo debes establecer";
        const string ContainerAlumno = "alumnos";
        const string ContainerTarea = "tareas-asignadas";
        const string ContainerTareaAlumno = "tareas-alumnos";
        const string SASQueryString = "este valor lo debes establecer";

        public async Task<string> UploadTarea(int id, Stream stream)
        {
            string blobSAS = $"{StorageURL}/{ContainerTarea}/{id}.pdf{SASQueryString}";
            return await UploadBlob(blobSAS, stream);
        }

        public async Task<string> UploadAlumno(int id, Stream stream)
        {
            string blobSAS = $"{StorageURL}/{ContainerAlumno}/{id}.jpg{SASQueryString}";
            return await UploadBlob(blobSAS, stream);
        }

        public async Task<Stream> DownloadAlumno(int id)
        {
            string blobSAS = $"{StorageURL}/{ContainerAlumno}/{id}.jpg{SASQueryString}";
            return await DownloadBlob(blobSAS);
        }

        public string GetFullDownloadTareaURL(int id)
        {
            return $"{StorageURL}/{ContainerTarea}/{id}.pdf{SASQueryString}";
        }

        public string GetFullDownloadAlumnoURL(int id)
        {
            return $"{StorageURL}/{ContainerAlumno}/{id}.pdf{SASQueryString}";
        }

        public string GetFullDownloadTareaAlumnoURL(int idTarea, int idAlumno)
        {
            return $"{StorageURL}/{ContainerTareaAlumno}/{idTarea}_{idAlumno}.pdf{SASQueryString}";
        }

        public async Task<Stream> DownloadTarea(int id)
        {
            string blobSAS = $"{StorageURL}/{ContainerTarea}/{id}.pdf{SASQueryString}";
            return await DownloadBlob(blobSAS);
        }

        public async Task<Stream> DownloadTareaAlumnos(int idTarea, int idAlumno)
        {
            string blobSAS = $"{StorageURL}/{ContainerTareaAlumno}/{idTarea}_{idAlumno}.pdf{SASQueryString}";
            return await DownloadBlob(blobSAS);
        }

        private async Task<Stream> DownloadBlob(string blobSAS)
        {
            try
            {
                CloudBlockBlob blob = new CloudBlockBlob(new Uri(blobSAS));
                MemoryStream stream = new MemoryStream();
                await blob.DownloadToStreamAsync(stream);
                return stream;
            }
            catch (Exception exc)
            {
                string msgError = exc.Message;
                return null;
            }
        }

        private async Task<string> UploadBlob(string blobSAS, Stream stream)
        {
            string url = "";

            try
            {
                CloudBlockBlob blob = new CloudBlockBlob(new Uri(blobSAS));

                using (stream)
                {
                    await blob.UploadFromStreamAsync(stream);
                    url = blob.StorageUri.PrimaryUri.AbsoluteUri;
                }
            }
            catch (Exception exc)
            {
                string msgError = exc.Message;
            }

            return url;
        }
    }
}