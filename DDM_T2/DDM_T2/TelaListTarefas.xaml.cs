using DDM_T2.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DDM_T2
{
    public class ListaTarefasViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Método para notificar alterações nas propriedades
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Propriedades específicas para este ViewModel
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _title = "Lista de Tarefas";
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _tarefas;
        public ObservableCollection<string> Tarefas
        {
            get => _tarefas;
            set
            {
                if (_tarefas != value)
                {
                    _tarefas = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AdicionarTarefaCommand { get; }

        // Construtor
        public ListaTarefasViewModel()
        {
            Tarefas = new ObservableCollection<string>();
            AdicionarTarefaCommand = new Command(AdicionarTarefa);
        }

        private async void AdicionarTarefa()
        {
            await Shell.Current.GoToAsync("CadastroTarefaPage");
        }
    }

}
