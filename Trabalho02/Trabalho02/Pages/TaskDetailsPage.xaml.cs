using System.Collections.ObjectModel;
using Trabalho02.Model;
using Trabalho02.Database;

namespace Trabalho02.Pages
{
    public partial class TaskDetailsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public Model.Task Task { get; set; }

        public TaskDetailsPage(Model.Task task, DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            Task = task;
            BindingContext = this;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Verifica alterações na tarefa
            var originalTask = await _databaseService.GetTaskByIdAsync(Task.Id);
            bool isNameChanged = originalTask.Title != Task.Title;
            bool isStatusChanged = originalTask.IsCompleted != Task.IsCompleted;

            if (isNameChanged || isStatusChanged)
            {
                // Atualiza no banco de dados
                await _databaseService.SaveTaskAsync(Task);
                await DisplayAlert("Sucesso", "Tarefa atualizada com sucesso!", "OK");
            }
            else
            {
                await DisplayAlert("Info", "Nenhuma alteração detectada.", "OK");
            }

            await Navigation.PopAsync();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            // Confirmação de exclusão
            bool confirm = await DisplayAlert("Excluir Tarefa", $"Deseja excluir a tarefa '{Task.Title}'?", "Sim", "Não");
            if (confirm)
            {
                await _databaseService.DeleteTaskAsync(Task);
                await DisplayAlert("Sucesso", "Tarefa excluída com sucesso!", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
