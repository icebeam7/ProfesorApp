using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfesorApp.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaMenu : ContentPage
    {
        public PaginaMenu()
        {
            InitializeComponent();
        }

        private async void Tareas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaginaListaTareas());
        }

        private async void Alumnos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaginaListaAlumnos());
        }

        private async void Respuestas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaginaListaTareasAlumnos());
        }
    }
}