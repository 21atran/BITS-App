using HtmlAgilityPack;

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
        var parNodes = htmlDoc.DocumentNode.SelectNodes("/");
        foreach (var node in parNodes.Nodes())
        {
            StackLayout chlidLayout = new StackLayout();
            if (node.Name=="p")
            {
                Label label = new Label()
                {
                    Text = node.InnerText,
                    FontSize = 14
                    // can format here
                };
                chlidLayout.Add(label);
                mainText.Children.Add(chlidLayout);
            }
            else if (node.Name=="#text")
            {
                Label label = new Label()
                {
                    Text = "",
                    FontSize = 7
                    // can format here
                };
                chlidLayout.Add(label);
                mainText.Children.Add(chlidLayout);
            }
            else if (node.Name=="figure")
            {
                Image img = new Image()
                {
                    Source = node.SelectSingleNode("//img").Attributes["src"].Value.ToString()
                    // can format here
                };
                Label label = new Label()
                {
                    Padding = 10,
                    Text = node.SelectSingleNode("//figcaption").InnerText,
                    FontSize = 10.5,
                    TextColor = Color.FromArgb("#000000"),
                    BackgroundColor = Color.FromArgb("#FFFFFF")
                    // can format here
                };
                chlidLayout.Add(img);
                chlidLayout.Add(label);
                mainText.Children.Add(chlidLayout);
            }
        }
    }

}