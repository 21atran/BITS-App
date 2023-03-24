using BITS_App.ViewModels;
using System.Runtime.CompilerServices;

namespace BITS_App.Pages;

public partial class StaffPage : ContentPage {
	public StaffPage() {
		InitializeComponent();
        BindingContext = new StaffViewModel();
        Dispatcher.Dispatch(async () => await ((StaffViewModel)BindingContext).RefreshAsync());
    }

    void OnTapGestureRecognizerTapped(object sender, TappedEventArgs e) {
        Dispatcher.Dispatch(async () => {
            await Navigation.PushAsync(new StaffProfilePage() {
                BindingContext = ((BindableObject)sender).BindingContext
            });
        });
    }
}