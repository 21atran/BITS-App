namespace BITS_App.Views;

public partial class StaffProfilePage : ContentPage {
	public StaffProfilePage() {
		InitializeComponent();

        BindingContext = new Models.StaffProfile(6889);

        Dispatcher.Dispatch(async () => await ((Models.StaffProfile)BindingContext).RefreshAsync());
    }
}