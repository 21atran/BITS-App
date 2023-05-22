namespace BITS_App.Pages;

public partial class SearchPage : ContentPage {
	public SearchPage() {
		InitializeComponent();
    }

    void OnTapGestureRecognizerTapped(object sender, TappedEventArgs e) {
        Dispatcher.Dispatch(async () => {
            await Navigation.PushAsync(new PostPage() {
                BindingContext = ((BindableObject)sender).BindingContext
            });
        });
    }
}