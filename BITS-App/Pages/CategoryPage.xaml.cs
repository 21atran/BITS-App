using BITS_App.ViewModels;

namespace BITS_App.Pages;

public partial class CategoryPage : ContentPage
{
	public CategoryPage()
	{
        InitializeComponent();
        BindingContext = new PostsViewModel()
        {
            Categories = new int[] { 11 }
        };

        Dispatcher.Dispatch(async () => await ((PostsViewModel)BindingContext).RefreshAsync());
    }

    void OnTapGestureRecognizerTapped(object sender, TappedEventArgs e)
    {
        Dispatcher.Dispatch(async () => {
            await Navigation.PushAsync(new PostPage()
            {
                BindingContext = ((BindableObject)sender).BindingContext
            });
        });
    }
}