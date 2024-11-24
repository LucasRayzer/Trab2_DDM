using Trabalho02.Model;
using Trabalho02.Database;

namespace Trabalho02.Pages
{
    public partial class HabitDetailsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public Habit Habit { get; set; }
        public Action HabitUpdatedCallback { get; set; } // Callback para notificar altera��es

        public HabitDetailsPage(Habit habit, DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            Habit = habit;
            BindingContext = this;
        }

        // Propriedade para exibir o texto do status "IsCompletedToday"
        public string IsCompletedTodayText => Habit.IsCompletedToday
            ? "Conclu�do hoje"
            : "N�o conclu�do hoje";

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Atualiza o h�bito no banco de dados
            await _databaseService.SaveHabitAsync(Habit);

            // Chama o callback para notificar que o h�bito foi atualizado
            HabitUpdatedCallback?.Invoke();

            await DisplayAlert("Sucesso", "H�bito atualizado com sucesso!", "OK");
            await Navigation.PopAsync(); // Volta para a tela anterior
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            // Confirma��o de exclus�o
            bool confirm = await DisplayAlert("Excluir H�bito", $"Deseja excluir o h�bito '{Habit.Title}'?", "Sim", "N�o");
            if (confirm)
            {
                await _databaseService.DeleteHabitAsync(Habit);

                // Chama o callback para notificar que o h�bito foi exclu�do
                HabitUpdatedCallback?.Invoke();

                await DisplayAlert("Sucesso", "H�bito exclu�do com sucesso!", "OK");
                await Navigation.PopAsync(); // Volta para a tela anterior
            }
        }
    }
}
