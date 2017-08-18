using System;
using ProfesorApp.Servicios;
using ProfesorApp.Clases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfesorApp.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaCalificarTareaAlumno : ContentPage
    {
        TareaAlumno dato;

        public PaginaCalificarTareaAlumno(TareaAlumno dato)
        {
            InitializeComponent();

            this.dato = dato;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ActualizarActivityIndicator(true);

            ServicioWebApi servicioWebApi = new ServicioWebApi();
            dato = await servicioWebApi.GetTareaAlumno(dato.IdTarea, dato.IdAlumno);
            this.BindingContext = dato;

            ActualizarActivityIndicator(false);
        }

        private void ActualizarActivityIndicator(bool estado)
        {
            activityIndicator.IsRunning = estado;
            activityIndicator.IsEnabled = estado;
            activityIndicator.IsVisible = estado;
        }

        private async void Calificar_Clicked(object sender, EventArgs e)
        {
            ActualizarActivityIndicator(true);

            dato.Evaluado = true;

            var servicioWebApi = new ServicioWebApi();
            await servicioWebApi.UpdateTareaAlumno(dato);

            ActualizarActivityIndicator(false);

            await DisplayAlert("Información", "Dato registrado con éxito", "OK");
            await Navigation.PopAsync();
        }

        private void VerTarea_Clicked(object sender, EventArgs e)
        {
            var servicioStorage = new ServicioStorage();
            Device.OpenUri(new Uri(servicioStorage.GetFullDownloadTareaURL(dato.IdTarea)));
        }

        private void VerRespuesta_Clicked(object sender, EventArgs e)
        {
            var servicioStorage = new ServicioStorage();
            Device.OpenUri(new Uri(servicioStorage.GetFullDownloadTareaAlumnoURL(dato.IdTarea, dato.IdAlumno)));
        }
    }
}