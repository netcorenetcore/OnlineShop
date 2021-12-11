using Online.Shop.Core.Data.Abstarctions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Online.Shop.Business.Data.Entities
{
   public  class Sale : EntityBase
    {
        [Required]
        public double Price { get; set; }
        public long? ProductId { get; set; }
        public long? StoreId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }



    }
}
