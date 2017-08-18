using System;
using ProfesorApp.Servicios;
using ProfesorApp.Clases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfesorApp.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaDetalleAlumno : ContentPage
    {
        Alumno dato;

        public PaginaDetalleAlumno(Alumno dato)
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

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            ActualizarActivityIndicator(true);

            var servicioWebApi = new ServicioWebApi();

            if (dato.Id == 0)
            {
                dato = await servicioWebApi.AddAlumno(dato);
            }
            else
            {
                await servicioWebApi.UpdateAlumno(dato);
            }

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
                    await servicioWebApi.DeleteAlumno(dato.Id);

                    ActualizarActivityIndicator(false);

                    await DisplayAlert("Información", "Dato eliminado con éxito", "OK");
                    await Navigation.PopAsync();
                }
            }
        }
    }
}