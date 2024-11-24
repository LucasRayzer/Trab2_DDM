using Trabalho02.Model;
using Trabalho02.Database;

namespace Trabalho02.Pages
{
    public partial class TaskDetailsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public Model.Task Task { get; set; }
        public Action TaskUpdatedCallback { get; set; }

        public TaskDetailsPage(Model.Task task, DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            Task = task;
            BindingContext = this;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await _databaseService.SaveTaskAsync(Task);

            TaskUpdatedCallback?.Invoke();

            await DisplayAlert("Sucesso", "Tarefa atualizada com sucesso!", "OK");
            await Navigation.PopAsync();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Excluir Tarefa", $"Deseja excluir a tarefa '{Task.Title}'?", "Sim", "Não");
            if (confirm)
            {
                await _databaseService.DeleteTaskAsync(Task);

                TaskUpdatedCallback?.Invoke();

                await DisplayAlert("Sucesso", "Tarefa excluída com sucesso!", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
