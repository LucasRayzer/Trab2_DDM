using System.Collections.ObjectModel;
using Trabalho02.Model;

namespace Trabalho02.Pages
{
    public partial class ProjectDetailsPage : ContentPage
    {
        public Project Project { get; set; }

        public ProjectDetailsPage(Project project)
        {
            InitializeComponent();

            Project = project;
            BindingContext = this;
            TasksListViews.ItemsSource = Project.Tasks;
        }

        private async void OnTaskSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Model.Task task)
            {
                await Navigation.PushAsync(new TaskDetailsPage(task));
            }
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            // Lógica para adicionar nova tarefa
            await DisplayAlert("Nova Tarefa", "Adicionar nova tarefa em desenvolvimento.", "OK");
        }
    }
}
