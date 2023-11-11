using Ejercicio_1._4.Models;

namespace Ejercicio_1._4.Views;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}

    private async void listapersonas_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var persona = (Empleado)e.Item;
        VerImagePage page = new VerImagePage();
        page.BindingContext = persona;
        await Navigation.PushAsync(page);
    }

    private async void toolmenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        listapersonas.ItemsSource = await App._DbContext.ObtenerListaEmpleados();
    }
}