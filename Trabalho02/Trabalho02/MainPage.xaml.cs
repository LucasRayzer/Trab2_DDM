using Trabalho02.Pages;

namespace Trabalho02
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToProjectsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProjectListPage());
        }

        private async void GoToProgressReportPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProgressReportPage());
        }
    }

}
