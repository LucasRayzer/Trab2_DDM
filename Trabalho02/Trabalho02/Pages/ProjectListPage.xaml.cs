using System.Collections.ObjectModel;
using Trabalho02.Model;

namespace Trabalho02.Pages
{
    public partial class ProjectListPage : ContentPage
    {
        public ObservableCollection<Project> Projects { get; set; }

        public ProjectListPage()
        {
            InitializeComponent();
            Projects = new ObservableCollection<Project>
        {
            new Project { Id = 1, Name = "Project 1", Description = "First project" },
            new Project { Id = 2, Name = "Project 2", Description = "Second project" }
        };
            ProjectsListView.ItemsSource = Projects;
        }

        private async void OnProjectSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Project project)
            {
                await Navigation.PushAsync(new ProjectDetailsPage(project));
            }
        }

        private async void OnAddProjectClicked(object sender, EventArgs e)
        {
            // Lógica para adicionar novo projeto
            await DisplayAlert("Novo Projeto", "Adicionar novo projeto em desenvolvimento.", "OK");
        }
    }
}
