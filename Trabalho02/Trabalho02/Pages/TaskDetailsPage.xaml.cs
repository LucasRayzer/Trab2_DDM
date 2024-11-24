using Trabalho02.Model;
using Trabalho02.Database;

namespace Trabalho02.Pages
{
    public partial class TaskDetailsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public Model.Task Task { get; set; }
        public Action TaskUpdatedCallback { get; set; } // Callback para notificar alterações

        public TaskDetailsPage(Model.Task task, DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            Task = task;
            BindingContext = this;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Atualiza a tarefa no banco de dados
            await _databaseService.SaveTaskAsync(Task);

            // Chama o callback para notificar que a tarefa foi atualizada
            TaskUpdatedCallback?.Invoke();

            await DisplayAlert("Sucesso", "Tarefa atualizada com sucesso!", "OK");
            await Navigation.PopAsync(); // Volta para a tela anterior
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            // Confirmação de exclusão
            bool confirm = await DisplayAlert("Excluir Tarefa", $"Deseja excluir a tarefa '{Task.Title}'?", "Sim", "Não");
            if (confirm)
            {
                await _databaseService.DeleteTaskAsync(Task);

                // Chama o callback para notificar que a tarefa foi excluída
                TaskUpdatedCallback?.Invoke();

                await DisplayAlert("Sucesso", "Tarefa excluída com sucesso!", "OK");
                await Navigation.PopAsync(); // Volta para a tela anterior
            }
        }
    }
}
