using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PoWebApi.Models
{
    public class PoLine
    {
        public int Id { get; set; }
        [Required]
        public int Quanity { get; set; }

        [Required]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        [Required]
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public PoLine() { }

    }
}
