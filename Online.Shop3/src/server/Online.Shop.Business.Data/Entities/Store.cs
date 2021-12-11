using Online.Shop.Core.Data.Abstarctions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Online.Shop.Business.Data.Entities
{
   public  class Store : EntityBase
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Address { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }


    }
}
