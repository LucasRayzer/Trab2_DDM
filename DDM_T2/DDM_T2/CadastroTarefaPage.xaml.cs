using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DDM_T2
{
    public partial class CadastroTarefaPage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _nomeTarefa;
        public string NomeTarefa
        {
            get => _nomeTarefa;
            set
            {
                if (_nomeTarefa != value)
                {
                    _nomeTarefa = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _prioridade;
        public string Prioridade
        {
            get => _prioridade;
            set
            {
                if (_prioridade != value)
                {
                    _prioridade = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SalvarTarefaCommand { get; }

        public CadastroTarefaPage()
        {
            SalvarTarefaCommand = new Command(SalvarTarefa);
        }

        private void SalvarTarefa()
        {
            // Lógica para salvar a tarefa (exemplo simplificado)
            System.Diagnostics.Debug.WriteLine($"Tarefa '{NomeTarefa}' com prioridade '{Prioridade}' salva.");
        }
    }

}
