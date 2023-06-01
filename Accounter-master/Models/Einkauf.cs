using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounter.Models
{
        [Table("Einkauf")]
        public class Einkauf
        {
            [PrimaryKey, AutoIncrement]
            public int BestellID { get; set; }
            public DateTime BestellDatum { get; set; }
            public int BestellAnzahl { get; set; }
            public decimal EinkaufsPreis { get; set; }
            public string Anmerkung { get; set; }
            public string Image { get; set; }
        }
    }

