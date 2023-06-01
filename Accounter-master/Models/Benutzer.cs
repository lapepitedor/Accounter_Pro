using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounter.ViewModels;
using SQLite;

namespace Accounter.Models
{
    [Table("Benutzer")]
    public class Benutzer 
    {
        //Properties
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Benutzername { get; set; }
        public int Passwort { get; set; }

    }
}
