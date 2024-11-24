using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Trabalho02.Converters
{
    public class TaskStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Verifica se o valor é um booleano
            if (value is bool isCompleted)
            {
                return isCompleted ? "Concluída" : "Em andamento";
            }

            return "Em andamento"; // Valor padrão
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
