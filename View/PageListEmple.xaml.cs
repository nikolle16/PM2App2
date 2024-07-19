using System.Collections.ObjectModel;
using PM2App2.Models;
using PM2App2.Controllers;

namespace PM2App2.View;

public partial class PageListEmple : ContentPage
{
    Models.Empleado selectedEmple;
    public Command<Empleado> UpdateCommand { get; }
    public Command<Empleado> DeleteCommand { get; }
    public ObservableCollection<Empleado> empleado { get; set; }

    public PageListEmple()
    {
		InitializeComponent();

        empleado = new ObservableCollection<Empleado>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        List<Models.Empleado> emplelist = new List<Models.Empleado>();
        emplelist = await EmpleadosController.Get();
        listemple.ItemsSource = emplelist;
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PageCreate());
    }

    private void listemple_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedEmple = e.CurrentSelection.FirstOrDefault() as Models.Empleado;
    }

    private async void Actualizar_Clicked(object sender, EventArgs e)
    {
        if (selectedEmple != null)
        {
            await Navigation.PushAsync(new actuEmple(selectedEmple.id));
        }
        else
        {
            await DisplayAlert("Error", "Seleccione una ubicacion primero", "OK");
        }
    }

    private async void Eliminar_Clicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Confirmar", "¿Está seguro que desea eliminar esta ubicacion?", "Sí", "No");

        if (selectedEmple != null)
        {
            if (result)
            {
                await EmpleadosController.Delete(selectedEmple.id);
                empleado.Remove(selectedEmple);

                var currentPage = this;
                await Navigation.PushAsync(new PageListEmple());
                Navigation.RemovePage(currentPage);
            }
            else
            {
                return;
            }
        }
        else
        {
            await DisplayAlert("Error", "Seleccione una ubicacion primero", "OK");
        }
    }
}