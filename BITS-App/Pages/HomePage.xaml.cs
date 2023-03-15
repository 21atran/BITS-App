using BITS_App.ViewModels;

namespace BITS_App.Pages;

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