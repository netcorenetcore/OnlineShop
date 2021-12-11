using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Online.Shop.Business.Contracts
{
    public  class StoreDto 
    {
        public long Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Address { get; set; }


    }
}
