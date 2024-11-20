using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho02.Model
{
    public class Statistic
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Type { get; set; }  // Tipo (ex.: "Tarefa", "Hábito")
        public DateTime Date { get; set; }  // Data de registro
        public string Detail { get; set; }  // Detalhes (ex.: "Tarefa concluída: 'Estudar'")
        public int TotalCompleted { get; set; }  // Total concluído no período
    }
}
