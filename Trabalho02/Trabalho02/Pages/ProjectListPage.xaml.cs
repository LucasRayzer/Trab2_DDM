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

            // Inicializa a coleção de projetos
            Projects = new ObservableCollection<Project>();
            ProjectsListView.ItemsSource = Projects;

            // Carrega os projetos do banco de dados

            LoadProjects();
        }

        private async void LoadProjects()
        {
            // Obtém os projetos do banco de dados
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
                // Navega para uma página de detalhes do projeto
                await Navigation.PushAsync(new ProjectDetailsPage(project, _databaseService));
            }

            // Deseleciona o item após o clique
            ProjectsListView.SelectedItem = null;
        }

        private async void OnAddProjectClicked(object sender, EventArgs e)
        {
            // Exibe um prompt para adicionar um novo projeto
            string name = await DisplayPromptAsync("Novo Projeto", "Nome do Projeto:");
            string description = await DisplayPromptAsync("Descrição do Projeto", "Descrição do Projeto:");

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(description))
            {
                // Cria e salva o novo projeto
                var newProject = new Project { Name = name, Description = description };
                await _databaseService.SaveProjectAsync(newProject);

                // Adiciona o projeto à lista exibida
                Projects.Add(newProject);
            }
        }

        private async void OnDeleteProjectClicked(object sender, EventArgs e)
{
    // Obtém o Id do projeto a ser excluído do CommandParameter
    var button = sender as Button;
    var projectId = (int)button.CommandParameter;

    // Busca o projeto pelo Id
    var projectToDelete = await _databaseService.GetProjectByIdAsync(projectId);

    if (projectToDelete != null)
    {
        // Confirmação de exclusão
        bool confirm = await DisplayAlert("Excluir Projeto", $"Deseja excluir o projeto '{projectToDelete.Name}'?", "Sim", "Não");
        if (confirm)
        {
            // Remove do banco de dados
            await _databaseService.DeleteProjectAsync(projectToDelete);

            // Recarrega a lista de projetos após a exclusão
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
