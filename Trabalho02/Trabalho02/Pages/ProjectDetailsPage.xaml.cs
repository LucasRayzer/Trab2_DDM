using System.Collections.ObjectModel;
using Trabalho02.Database;
using Trabalho02.Model;

namespace Trabalho02.Pages
{
    public partial class ProjectDetailsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public Project Project { get; set; }
        public ObservableCollection<Model.Task> Tasks { get; set; }

        public ProjectDetailsPage(Project project, DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;

            // Inicializa a coleção de tarefas
            Tasks = new ObservableCollection<Model.Task>();
            TasksListView.ItemsSource = Tasks;

            // Defina o projeto no BindingContext
            Project = project;
            BindingContext = this;

            // Carrega as tarefas associadas ao projeto
            LoadTasks();
        }

        private async void LoadTasks()
        {
            // Busca as tarefas associadas ao projeto
            var tasks = await _databaseService.GetTasksByProjectIdAsync(Project.Id);
            Tasks.Clear();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        private async void OnTaskSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Model.Task task)
            {
                // Navega para a página de detalhes da tarefa
                await Navigation.PushAsync(new TaskDetailsPage(task));
            }

            // Deseleciona o item após o clique
            TasksListView.SelectedItem = null;
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            // Exibe um prompt para adicionar uma nova tarefa
            string title = await DisplayPromptAsync("Nova Tarefa", "Título da tarefa:");
            string description = await DisplayPromptAsync("Descrição da Tarefa", "Descrição da tarefa:");
            DateTime dueDate = DateTime.Now;

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description))
            {
                // Cria uma nova tarefa
                var newTask = new Model.Task
                {
                    Title = title,
                    Description = description,
                    DueDate = dueDate,
                    IsCompleted = false,
                    ProgressStatus = "Em andamento",
                    ProjectId = Project.Id
                };

                // Salva a nova tarefa no banco de dados
                await _databaseService.SaveTaskAsync(newTask);

                // Adiciona a nova tarefa na lista exibida
                Tasks.Add(newTask);
            }
        }
    }
}
