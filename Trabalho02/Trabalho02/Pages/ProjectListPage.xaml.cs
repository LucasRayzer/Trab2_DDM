using System.Collections.ObjectModel;
using Trabalho02.Database;
using Trabalho02.Model;

namespace Trabalho02.Pages
{
    public partial class ProjectListPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<Project> Projects { get; set; }

        public ProjectListPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;

            Projects = new ObservableCollection<Project>();
            ProjectsListView.ItemsSource = Projects;


            LoadProjects();
        }

        private async void LoadProjects()
        {
            var projects = await _databaseService.GetProjectsAsync();
            Projects.Clear();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }

        private async void OnProjectSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Project project)
            {
                await Navigation.PushAsync(new ProjectDetailsPage(project, _databaseService));
            }

            ProjectsListView.SelectedItem = null;
        }

        private async void OnAddProjectClicked(object sender, EventArgs e)
        {
            string name = await DisplayPromptAsync("Novo Projeto", "Nome do Projeto:");
            string description = await DisplayPromptAsync("Descrição do Projeto", "Descrição do Projeto:");

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(description))
            {
                var newProject = new Project { Name = name, Description = description };
                await _databaseService.SaveProjectAsync(newProject);

                Projects.Add(newProject);
            }
        }

        private async void OnDeleteProjectClicked(object sender, EventArgs e)
{
    var button = sender as Button;
    var projectId = (int)button.CommandParameter;

    var projectToDelete = await _databaseService.GetProjectByIdAsync(projectId);

    if (projectToDelete != null)
    {
        bool confirm = await DisplayAlert("Excluir Projeto", $"Deseja excluir o projeto '{projectToDelete.Name}'?", "Sim", "Não");
        if (confirm)
        {
            await _databaseService.DeleteProjectAsync(projectToDelete);

            LoadProjects();
        }
    }
    else
    {
        await DisplayAlert("Erro", "Projeto não encontrado.", "OK");
    }
}

    }
}
