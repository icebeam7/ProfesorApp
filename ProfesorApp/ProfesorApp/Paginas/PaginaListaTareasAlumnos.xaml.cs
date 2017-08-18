using System;
using ProfesorApp.Servicios;
using ProfesorApp.Clases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfesorApp.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaListaTareasAlumnos : ContentPage
    {
        public PaginaListaTareasAlumnos()
        {
            InitializeComponent();
        }

        private void ActualizarActivityIndicator(bool estado)
        {
            activityIndicator.IsRunning = estado;
            activityIndicator.IsEnabled = estado;
            activityIndicator.IsVisible = estado;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ActualizarActivityIndicator(true);

            var servicioWebApi = new ServicioWebApi();
            lsvTareasAlumnos.ItemsSource = await servicioWebApi.GetTareaAlumnos();

            ActualizarActivityIndicator(false);
        }

        private async void lsvTareasAlumnos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                TareaAlumno dato = (TareaAlumno)e.SelectedItem;
                await Navigation.PushAsync(new PaginaCalificarTareaAlumno(dato));
            }
            catch (Exception ex)
            {
            }
        }
    }
}