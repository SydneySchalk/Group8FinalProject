using System.Diagnostics;

namespace Final
{
    public partial class App : Application
    {
        public static DatabaseAccess Database { get; private set; }
        public App()
        {
            try
            {
                InitializeComponent();
                Database = new DatabaseAccess();
                Task.Run(async () => await Database.Init());
                MainPage = new MainPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"App constructor failed: {ex}");
                throw;
            }
        }

    }
}