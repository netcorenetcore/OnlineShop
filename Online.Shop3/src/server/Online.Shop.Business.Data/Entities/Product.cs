using Online.Shop.Core.Data.Abstarctions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Online.Shop.Business.Data.Entities
{
   public  class Product:EntityBase
    {
        [Required]
        public long BarcodeNumber { get; set; }
        [StringLength(16)]
        public string Name { get; set; }
        public double TPrice { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }



    }
}
