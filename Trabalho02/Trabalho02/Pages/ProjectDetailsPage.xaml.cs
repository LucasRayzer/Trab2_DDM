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

            Tasks = new ObservableCollection<Model.Task>();
            TasksListView.ItemsSource = Tasks;

            Habits = new ObservableCollection<Habit>();
            HabitsListView.ItemsSource = Habits;

            Project = project;
            BindingContext = this;

            LoadTasks();
            LoadHabits();
        }

        private async System.Threading.Tasks.Task LoadTasks()
        {
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
                var taskDetailsPage = new TaskDetailsPage(task, _databaseService)
                {
                    TaskUpdatedCallback = async () =>
                    {
                        await LoadTasks();
                    }
                };

                await Navigation.PushAsync(taskDetailsPage);
            }

            TasksListView.SelectedItem = null;
        }


        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            string title = await DisplayPromptAsync("Nova Tarefa", "Título da tarefa:");
            string description = await DisplayPromptAsync("Descrição da Tarefa", "Descrição da tarefa:");
            DateTime dueDate = DateTime.Now;

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description))
            {
                var newTask = new Model.Task
                {
                    Title = title,
                    Description = description,
                    DueDate = dueDate,
                    IsCompleted = false,
                    ProgressStatus = "Em andamento",
                    ProjectId = Project.Id
                };

                await _databaseService.SaveTaskAsync(newTask);

                Tasks.Add(newTask);
            }
        }
        private async void LoadHabits()
        {
            var habits = await _databaseService.GetHabitsByProjectIdAsync(Project.Id);
            Habits.Clear();
            foreach (var habit in habits)
            {
                Habits.Add(habit);
            }
        }

        private async void OnAddHabitClicked(object sender, EventArgs e)
        {
            var taskTitles = Tasks.Select(t => t.Title).ToArray();
            string selectedTaskTitle = await DisplayActionSheet("Selecione a Tarefa", "Cancelar", null, taskTitles);

            if (string.IsNullOrEmpty(selectedTaskTitle) || selectedTaskTitle == "Cancelar")
                return;

            var selectedTask = Tasks.FirstOrDefault(t => t.Title == selectedTaskTitle);
            if (selectedTask == null) return;

            string title = await DisplayPromptAsync("Novo Hábito", "Título do Hábito:");
            string description = await DisplayPromptAsync("Descrição do Hábito", "Descrição do Hábito:");
            string frequency = await DisplayPromptAsync("Frequência", "Informe a frequência (ex.: Diário):");
            string goal = await DisplayPromptAsync("Meta", "Informe a meta (ex.: 3):");

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description))
            {
                var newHabit = new Habit
                {
                    Title = title,
                    Description = description,
                    Frequency = frequency,
                    Goal = goal,
                    Progress = 0,
                    IsCompletedToday = false,
                    TaskId = selectedTask.Id 
                };

                await _databaseService.SaveHabitAsync(newHabit);

                Habits.Add(newHabit);
            }
        }
        private async void OnHabitSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Habit selectedHabit)
            {
                await Navigation.PushAsync(new HabitDetailsPage(selectedHabit, _databaseService)
                {
                    HabitUpdatedCallback = LoadHabits
                });
            }

            HabitsListView.SelectedItem = null;
        }

    }


}

