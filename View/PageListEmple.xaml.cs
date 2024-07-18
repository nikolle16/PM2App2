

namespace PM2App2.View;

public partial class PageListEmple : ContentPage
{
	public PageListEmple()
	{
		InitializeComponent();
	}

    /*private async void btntest_Clicked(object sender, EventArgs e)
    {
		List<Models.Empleado> emplelist = new List<Models.Empleado>();

		emplelist = await Controllers.EmpleadosController.Get();
    }*/

    private void listemple_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        List<Models.Empleado> emplelist = new List<Models.Empleado>();
        emplelist = await Controllers.EmpleadosController.Get();
        listemple.ItemsSource = emplelist;
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PageCreate());
    }
}