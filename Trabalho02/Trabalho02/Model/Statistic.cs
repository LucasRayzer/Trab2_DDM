﻿using SQLite;
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

        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Detail { get; set; }
        public int TotalCompleted { get; set; }
    }
}
