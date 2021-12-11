using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Online.Shop.Business.Contracts
{
    public  class SaleDto 
    {
        public long Id { get; set; }

        [Required]
        public double Price { get; set; }
        public long? ProductId { get; set; }
        public long? StoreId { get; set; }
        public long ProductBarcodeNumber { get; set; }
        public string StoreName { get; set; }



    }
}
