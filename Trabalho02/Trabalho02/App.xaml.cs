using Trabalho02.Database;
using Trabalho02.Pages;
using Trabalho02.Converters;

namespace Trabalho02
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "AppData.db");
            var databaseService = new DatabaseService(dbPath);
            MainPage = new NavigationPage(new LoginPage(databaseService));
            TaskNameConverter.Initialize(databaseService);
            //MainPage = new AppShell();
            //MainPage = new NavigationPage(new LoginPage());
        }
    }
}
