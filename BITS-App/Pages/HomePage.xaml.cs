namespace BITS_App.Pages;
using BITS_App.ViewModels;
using BITS_App.Views;

public partial class HomePage : ContentPage {
	public HomePage() {
		InitializeComponent();
        BindingContext = new PostsViewModel();
        Dispatcher.Dispatch(async () => await ((PostsViewModel)BindingContext).RefreshAsync());
    }

    void OnTapGestureRecognizerTapped(object sender, TappedEventArgs e) {
        Dispatcher.Dispatch(async () => { 
            await Navigation.PushAsync(new PostPage() {
                BindingContext = ((BindableObject)sender).BindingContext
            }); 
        });
    }
}