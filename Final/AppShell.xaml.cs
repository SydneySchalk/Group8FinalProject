using Final.Views;

namespace Final
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FishDetailsPage), typeof(FishDetailsPage));
        }
    }
}
