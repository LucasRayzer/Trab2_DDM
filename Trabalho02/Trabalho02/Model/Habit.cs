using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Trabalho02.Model
{
    public class Habit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }  // Nome do hábito
        public string Description { get; set; }  // Descrição do hábito
        public string Frequency { get; set; }  // Frequência (ex.: "Diário", "Semanal")
        public int Goal { get; set; }  // Meta (ex.: "3 vezes por semana")
        public int Progress { get; set; }  // Progresso atual
        public bool IsCompletedToday { get; set; }  // Status do dia atual
        // Associação com Task
        public int TaskId { get; set; }  // ID da tarefa associada 
    }
}
