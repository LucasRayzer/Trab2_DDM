using Trabalho02.Model;
using Trabalho02.Database;

namespace Trabalho02.Pages
{
    public partial class HabitDetailsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public Habit Habit { get; set; }
        public Action HabitUpdatedCallback { get; set; }

        public HabitDetailsPage(Habit habit, DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            Habit = habit;
            BindingContext = this;
        }

        public string IsCompletedTodayText => Habit.IsCompletedToday
            ? "Concluído hoje"
            : "Não concluído hoje";

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await _databaseService.SaveHabitAsync(Habit);

            HabitUpdatedCallback?.Invoke();

            await DisplayAlert("Sucesso", "Hábito atualizado com sucesso!", "OK");
            await Navigation.PopAsync();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Excluir Hábito", $"Deseja excluir o hábito '{Habit.Title}'?", "Sim", "Não");
            if (confirm)
            {
                await _databaseService.DeleteHabitAsync(Habit);

                HabitUpdatedCallback?.Invoke();

                await DisplayAlert("Sucesso", "Hábito excluído com sucesso!", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
