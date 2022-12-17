namespace BITS_App.Views;

public partial class PostPanelView : ContentView
{
    public PostPanelView(int id)
    {
        InitializeComponent();

        BindingContext = new Models.Post(id);
        Dispatcher.Dispatch(async () => await ((Models.Post)BindingContext).RefreshAsync());

    }
}