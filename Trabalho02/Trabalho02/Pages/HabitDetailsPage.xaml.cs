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
            ? "Conclu�do hoje"
            : "N�o conclu�do hoje";

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await _databaseService.SaveHabitAsync(Habit);

            HabitUpdatedCallback?.Invoke();

            await DisplayAlert("Sucesso", "H�bito atualizado com sucesso!", "OK");
            await Navigation.PopAsync();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Excluir H�bito", $"Deseja excluir o h�bito '{Habit.Title}'?", "Sim", "N�o");
            if (confirm)
            {
                await _databaseService.DeleteHabitAsync(Habit);

                HabitUpdatedCallback?.Invoke();

                await DisplayAlert("Sucesso", "H�bito exclu�do com sucesso!", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
