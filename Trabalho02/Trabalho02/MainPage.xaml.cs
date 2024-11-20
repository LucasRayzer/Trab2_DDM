using Trabalho02.Database;
using Trabalho02.Pages;

namespace Trabalho02
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;  
        public MainPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void GoToProjectsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProjectListPage(_databaseService));
        }

        private async void GoToProgressReportPage(object sender, EventArgs e)   
        {
            await Navigation.PushAsync(new ProgressReportPage());
        }
    }

}
