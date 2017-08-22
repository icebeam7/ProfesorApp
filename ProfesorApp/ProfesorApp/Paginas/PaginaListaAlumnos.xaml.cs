using System;
using ProfesorApp.Servicios;
using ProfesorApp.Clases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfesorApp.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaListaAlumnos : ContentPage
    {
        public PaginaListaAlumnos()
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
            lsvAlumnos.ItemsSource = await ServicioWebApi.GetAlumnos();
            ActualizarActivityIndicator(false);
        }

        private async void lsvAlumnos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Alumno dato = (Alumno)e.SelectedItem;
                await Navigation.PushAsync(new PaginaDetalleAlumno(dato));
            }
            catch (Exception ex)
            {
            }
        }

        private async void Agregar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaginaDetalleAlumno(new Alumno()));
        }
    }
}