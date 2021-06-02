using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoWebApi.Models
{
    public class Employee
    {
        public int Id { get; set; } //primary key
        public string Login { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsManager { get; set; } //will default to false when not stated other wise

        public Employee() { } //constructor, probably dont need but doesnt hurt to have

    }
}
