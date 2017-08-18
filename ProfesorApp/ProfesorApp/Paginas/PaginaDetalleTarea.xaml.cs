using System;
using ProfesorApp.Servicios;
using ProfesorApp.Clases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace ProfesorApp.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaDetalleTarea : ContentPage
    {
        Tarea dato;
        MemoryStream stream;

        public PaginaDetalleTarea(Tarea dato)
        {
            InitializeComponent();

            ActualizarActivityIndicator(true);

            this.dato = dato;
            this.BindingContext = dato;

            ActualizarActivityIndicator(false);
        }

        private void ActualizarActivityIndicator(bool estado)
        {
            activityIndicator.IsRunning = estado;
            activityIndicator.IsEnabled = estado;
            activityIndicator.IsVisible = estado;
        }

        private async void btnArchivo_Clicked(object sender, EventArgs e)
        {
            ServicioFilePicker servicioFilePicker = new ServicioFilePicker();
            stream = await servicioFilePicker.GetFile();
        }

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            ActualizarActivityIndicator(true);

            var servicioStorage = new ServicioStorage();
            var servicioWebApi = new ServicioWebApi();

            if (dato.Id == 0)
            {
                dato = await servicioWebApi.AddTarea(dato);
            }

            dato.ArchivoURL = await servicioStorage.UploadTarea(dato.Id, stream);
            await servicioWebApi.UpdateTarea(dato);

            ActualizarActivityIndicator(false);

            await DisplayAlert("Información", "Dato registrado con éxito", "OK");
            await Navigation.PopAsync();
        }

        private async void Eliminar_Clicked(object sender, EventArgs e)
        {
            if (dato.Id > 0)
            {
                if (await DisplayAlert("Eliminar", "¿Deseas eliminar el registro?", "Si", "No"))
                {
                    ActualizarActivityIndicator(true);

                    var servicioWebApi = new ServicioWebApi();
                    await servicioWebApi.DeleteTarea(dato.Id);

                    ActualizarActivityIndicator(false);

                    await DisplayAlert("Información", "Dato eliminado con éxito", "OK");
                    await Navigation.PopAsync();
                }
            }
        }

        private void Ver_Clicked(object sender, EventArgs e)
        {
            if (dato.Id > 0)
            {
                var servicioStorage = new ServicioStorage();
                Device.OpenUri(new Uri(servicioStorage.GetFullDownloadTareaURL(dato.Id)));
                //var stream = await servicioStorage.DownloadTarea(dato.Id);
            }
        }
    }
}