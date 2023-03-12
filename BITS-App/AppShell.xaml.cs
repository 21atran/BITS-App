using System.Windows.Input;

namespace BITS_App;

public partial class AppShell : Shell {

    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

    public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
    public AppShell() {
		InitializeComponent();
        RegisterRoutes();
        BindingContext = this;
    }
    void RegisterRoutes()
    {
        Routes.Add("Post", typeof(Views.PostPage));

        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }
    }
}
