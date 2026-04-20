using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class ProductInventory
    {
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        [ForeignKey("InventoryId")]
        public int InventoryId { get; set; }
        [JsonIgnore]
        public virtual Inventory Inventory { get; set; }

        //public int ProductId { get; set; }
        
        //[JsonIgnore]
        //public Product? Product { get; set; }
        //public int InventoryId { get; set; }

        //[JsonIgnore]
        //public Inventory? Inventory { get; set; }
        public int InventoryQuantity { get; set; }


    }
}
