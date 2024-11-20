using Trabalho02.Database;
using Trabalho02.Pages;

namespace Trabalho02
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "AppData.db");
            var databaseService = new DatabaseService(dbPath);

            // Define a LoginPage como a página inicial, passando o DatabaseService
            MainPage = new NavigationPage(new LoginPage(databaseService));
            //MainPage = new AppShell();
            //MainPage = new NavigationPage(new LoginPage());
        }
    }
}
