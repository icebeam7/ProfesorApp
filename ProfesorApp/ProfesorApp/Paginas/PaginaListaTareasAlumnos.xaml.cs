using System;
using ProfesorApp.Servicios;
using ProfesorApp.Clases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

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

        private async Task ObtenerTareasAlumnos()
        {
            ActualizarActivityIndicator(true);
            lsvTareasAlumnos.ItemsSource = await ServicioWebApi.GetTareaAlumnosByEval(switchTareaEvaluada.IsToggled);
            ActualizarActivityIndicator(false);
        }

        private async void switchTareaEvaluada_Toggled(object sender, ToggledEventArgs e)
        {
            await ObtenerTareasAlumnos();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                await ObtenerTareasAlumnos();
            }
            catch(Exception ex)
            {

            }
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