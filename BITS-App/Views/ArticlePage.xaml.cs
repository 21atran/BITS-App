namespace BITS_App.Views;

public partial class ArticlePage : ContentPage
{
	public ArticlePage()
	{
		InitializeComponent();

        BindingContext = new Models.Article(7767);
    }
}