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
        public ObservableCollection<Habit> Habits { get; set; }

        public ProjectDetailsPage(Project project, DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;

            // Inicializa a coleção de tarefas
            Tasks = new ObservableCollection<Model.Task>();
            TasksListView.ItemsSource = Tasks;

            // Inicializa a coleção de hábitos
            Habits = new ObservableCollection<Habit>();
            HabitsListView.ItemsSource = Habits;

            // Defina o projeto no BindingContext
            Project = project;
            BindingContext = this;

            // Carrega as tarefas associadas ao projeto
            LoadTasks();
            // Carrega os hábitos associados ao projeto
            LoadHabits();
        }

        private async System.Threading.Tasks.Task LoadTasks()
        {
            // Busca as tarefas associadas ao projeto no banco de dados
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
                // Navega para a TaskDetailsPage e configura o callback
                var taskDetailsPage = new TaskDetailsPage(task, _databaseService)
                {
                    TaskUpdatedCallback = async () =>
                    {
                        // Atualiza a lista de tarefas quando alterações forem feitas
                        await LoadTasks();
                    }
                };

                await Navigation.PushAsync(taskDetailsPage);
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
        private async void LoadHabits()
        {
            // Busca os hábitos associados ao projeto
            var habits = await _databaseService.GetHabitsByProjectIdAsync(Project.Id);
            Habits.Clear();
            foreach (var habit in habits)
            {
                Habits.Add(habit);
            }
        }

        private async void OnAddHabitClicked(object sender, EventArgs e)
        {
            // Permite ao usuário selecionar uma tarefa
            var taskTitles = Tasks.Select(t => t.Title).ToArray();
            string selectedTaskTitle = await DisplayActionSheet("Selecione a Tarefa", "Cancelar", null, taskTitles);

            if (string.IsNullOrEmpty(selectedTaskTitle) || selectedTaskTitle == "Cancelar")
                return;

            // Encontra a tarefa selecionada
            var selectedTask = Tasks.FirstOrDefault(t => t.Title == selectedTaskTitle);
            if (selectedTask == null) return;

            // Exibe prompts para criar o hábito
            string title = await DisplayPromptAsync("Novo Hábito", "Título do Hábito:");
            string description = await DisplayPromptAsync("Descrição do Hábito", "Descrição do Hábito:");
            string frequency = await DisplayPromptAsync("Frequência", "Informe a frequência (ex.: Diário):");
            string goal = await DisplayPromptAsync("Meta", "Informe a meta (ex.: 3):");

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description))
            {
                // Cria um novo hábito associado à tarefa
                var newHabit = new Habit
                {
                    Title = title,
                    Description = description,
                    Frequency = frequency,
                    Goal = goal,
                    Progress = 0,
                    IsCompletedToday = false,
                    TaskId = selectedTask.Id // Associa à tarefa
                };

                // Salva o hábito no banco de dados
                await _databaseService.SaveHabitAsync(newHabit);

                // Adiciona o hábito na lista exibida
                Habits.Add(newHabit);
            }
        }
        private async void OnHabitSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Habit selectedHabit)
            {
                // Abre a HabitDetailsPage
                await Navigation.PushAsync(new HabitDetailsPage(selectedHabit, _databaseService)
                {
                    HabitUpdatedCallback = LoadHabits // Recarrega a lista de hábitos ao retornar
                });
            }

            // Deseleciona o item
            HabitsListView.SelectedItem = null;
        }

    }


}

