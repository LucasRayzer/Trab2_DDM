using Trabalho02.Model;
using Trabalho02.Database;

namespace Trabalho02.Pages
{
    public partial class HabitDetailsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public Habit Habit { get; set; }
        public Action HabitUpdatedCallback { get; set; } // Callback para notificar alterações

        public HabitDetailsPage(Habit habit, DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            Habit = habit;
            BindingContext = this;
        }

        // Propriedade para exibir o texto do status "IsCompletedToday"
        public string IsCompletedTodayText => Habit.IsCompletedToday
            ? "Concluído hoje"
            : "Não concluído hoje";

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Atualiza o hábito no banco de dados
            await _databaseService.SaveHabitAsync(Habit);

            // Chama o callback para notificar que o hábito foi atualizado
            HabitUpdatedCallback?.Invoke();

            await DisplayAlert("Sucesso", "Hábito atualizado com sucesso!", "OK");
            await Navigation.PopAsync(); // Volta para a tela anterior
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            // Confirmação de exclusão
            bool confirm = await DisplayAlert("Excluir Hábito", $"Deseja excluir o hábito '{Habit.Title}'?", "Sim", "Não");
            if (confirm)
            {
                await _databaseService.DeleteHabitAsync(Habit);

                // Chama o callback para notificar que o hábito foi excluído
                HabitUpdatedCallback?.Invoke();

                await DisplayAlert("Sucesso", "Hábito excluído com sucesso!", "OK");
                await Navigation.PopAsync(); // Volta para a tela anterior
            }
        }
    }
}
