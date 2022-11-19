using HtmlAgilityPack;
using static System.Net.Mime.MediaTypeNames;

namespace BITS_App.Views;

public partial class ArticlePage : ContentPage
{
    public ArticlePage()
	{
		InitializeComponent();

        BindingContext = new Models.Article(7767);

        var html = ((Models.Article)BindingContext).Content;
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        var parNodes = htmlDoc.DocumentNode.SelectNodes("/p");
        foreach (var node in parNodes)
        {
            Label label = new Label()
            {
                Text = node.InnerText,
                FontSize = 14
                // can format here
            };
            mainText.Children.Add(label);
        }
    }

}