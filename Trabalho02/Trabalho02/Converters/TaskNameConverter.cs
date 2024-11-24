using System;
using System.Globalization;
using System.Threading.Tasks;
using Trabalho02.Database;

namespace Trabalho02.Converters
{
    public class TaskNameConverter : IValueConverter
    {
        private static DatabaseService _databaseService;

        public static void Initialize(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int taskId && _databaseService != null)
            {
                // Retorna uma Task com título
                var task = Task.Run(() => _databaseService.GetTaskByIdAsync(taskId)).Result;
                return task?.Title ?? "Tarefa Desconhecida";
            }

            return "Tarefa Desconhecida";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
