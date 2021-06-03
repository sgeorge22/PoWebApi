using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PoWebApi.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        [Required, StringLength(30)]
        public string Description { get; set; } // why is the user putting in the order
        [Required, StringLength(20)]
        public string Status { get; set; } = "NEW"; // info not input by the user but updated through the application (program code), default new
        [Column(TypeName = "decimal(9,2)")] // this is communicating that column is a decimal and the 9 is total digits and 2 is how many digits come after .
        //TypeName is used to communicate to SQL
        public decimal Total { get; set; } = 0;// grand total of all items added to the purchase order, default to 0
        [Required]
        public bool Active { get; set; } = true;//bools default to false so adding ? defaults to true

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public PurchaseOrder() { }
    }
}
