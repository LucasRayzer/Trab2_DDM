using System.Collections.ObjectModel;
using Trabalho02.Model;

namespace Trabalho02.Pages
{
    public partial class TaskDetailsPage : ContentPage
    {
        public Model.Task Task { get; set; }

        public TaskDetailsPage(Model.Task task)
        {
            InitializeComponent();
            Task = task;
            BindingContext = this;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Aqui você pode salvar a tarefa atualizada no banco de dados ou em um serviço
            await DisplayAlert("Success", "Task details saved successfully!", "OK");
            await Navigation.PopAsync();
        }
    }
}
