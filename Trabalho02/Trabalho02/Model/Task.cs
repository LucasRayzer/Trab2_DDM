using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho02.Model
{
    public class Task
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }  // Título da tarefa
        public string Description { get; set; }  // Descrição da tarefa
        public DateTime DueDate { get; set; }  // Data de conclusão
        public bool IsCompleted { get; set; }  // Status de conclusão
        public string ProgressStatus { get; set; }  // Status de progresso (ex.: "Em andamento", "Atrasada")
        public int ProjectId { get; set; }
    }
}
