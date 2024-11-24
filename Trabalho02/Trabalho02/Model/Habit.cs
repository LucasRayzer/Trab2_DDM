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

        public string Title { get; set; }
        public string Description { get; set; }
        public string Frequency { get; set; }
        public string Goal { get; set; }
        public int Progress { get; set; }
        public bool IsCompletedToday { get; set; }
        public int TaskId { get; set; }
    }
}
