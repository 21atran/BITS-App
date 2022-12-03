namespace BITS_App;

public partial class App : Application
{
	// set to client base url
	public const string BASE_URL = "gwhsnews.org";
	
	public static HttpClient client { get; private set; }

	public App()
	{
		InitializeComponent();

		client = new HttpClient();

		MainPage = new AppShell();
	}
}
