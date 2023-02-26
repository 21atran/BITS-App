namespace BITS_App.Pages;
using BITS_App.ViewModels;

public partial class HomePage : ContentPage {
	public HomePage() {
		InitializeComponent();
        BindingContext = new PostsViewModel();
        Dispatcher.Dispatch(async () => await ((PostsViewModel)BindingContext).RefreshAsync());
    }
}