using HtmlAgilityPack;
namespace BITS_App.Views;

public partial class ArticlePage : ContentPage
{
    List<string> paragraphs;
    public ArticlePage()
	{
		InitializeComponent();

        BindingContext = new Models.Article(7767);

        var html = ((Models.Article)BindingContext).Content;
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        var contentNode = htmlDoc.DocumentNode.SelectSingleNode("//content//rendered");
        var parNodes = htmlDoc.DocumentNode.SelectNodes("/p");
        paragraphs = new List<string>();
        foreach (var node in parNodes)
        {
            paragraphs.Add(node.InnerText);
        }

        var labels = CreateLabels(paragraphs);
        StackLayout childLayout;
        foreach(var label in labels)
        {
            childLayout = new StackLayout();
            childLayout.Children.Add(label);
            mainText.Children.Add(childLayout);
        }

    }

    public List<Label> CreateLabels(List<string> texts){
        List<Label> paraLabels = new List<Label>();
        foreach(string text in texts)
        {
            Label label = new Label()
            {
                Text = text,
                FontSize=14
                // can format here
            };
            paraLabels.Add(label);
        }
        return paraLabels;
    }

}