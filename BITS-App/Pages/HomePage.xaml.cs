namespace BITS_App.Pages;
using BITS_App.ViewModels;
using BITS_App.Views;
using System.Diagnostics;

public partial class HomePage : ContentPage {
	public HomePage() {
		InitializeComponent();
        BindingContext = new PostsViewModel();
        Dispatcher.Dispatch(async () => await ((PostsViewModel)BindingContext).RefreshAsync());
    }

    void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int selected = ((Models.Post)(e.CurrentSelection[0])).Id ?? -1;
        Debug.WriteLine(selected);
        Debug.WriteLine((object)Shell.Current ?? (object)"null");
        Dictionary<string, object> navigationParameter = new Dictionary<string, object>
        {
            { "id", selected }
        };
        Dispatcher.Dispatch(async () => { await Navigation.PushAsync(new PostPage(selected)); }) ;
        

    }
}