namespace BITS_App.Views;
using HtmlAgilityPack;
public partial class ArticlePage : ContentPage
{
	public ArticlePage()
	{
		InitializeComponent();

        BindingContext = new Models.Article(7767);
    }
}