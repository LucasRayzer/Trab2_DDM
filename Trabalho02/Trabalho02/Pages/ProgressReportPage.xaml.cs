using System.Collections.ObjectModel;
using Trabalho02.Model;

namespace Trabalho02.Pages
{
    public partial class ProgressReportPage : ContentPage
    {
        public double ProjectCompletionRate { get; set; }
        public string ProjectCompletionPercentage => $"{ProjectCompletionRate:P0}";

        public ProgressReportPage()
        {
            InitializeComponent();

            ProjectCompletionRate = CalculateCompletionRate();
            BindingContext = this;
        }

        private double CalculateCompletionRate()
        {
            // Lógica para calcular o progresso com base nas tarefas completas e totais
            int completedTasks = 8; // Exemplo estático
            int totalTasks = 10; // Exemplo estático
            return (double)completedTasks / totalTasks;
        }
    }
}
