using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PoWebApi.Models
{
    public class Employee
    {
        
        public int Id { get; set; } //primary key
        [Required, StringLength(30)]
        public string Login { get; set; }
        [Required, StringLength(30)]
        public string Password { get; set; }
        [Required, StringLength(30)]
        public string Firstname { get; set; }
        [Required, StringLength(30)]
        public string Lastname { get; set; }
        [Required]
        public bool IsManager { get; set; } //will default to false when not stated other wise

        public Employee() { } //constructor, probably dont need but doesnt hurt to have

    }
}
