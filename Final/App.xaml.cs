namespace Final
{
    public partial class App : Application
    {
        public static DatabaseAccess Database { get; private set; }
        public App()
        {
            InitializeComponent();

            Database = new DatabaseAccess();
            Database.Init().Wait();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}