using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Online.Shop.Business.Contracts
{
    public class ProductDto
    {
        public long Id { get; set; }
        [Required]
        public long BarcodeNumber { get; set; }
        [StringLength(16)]
        public string Name { get; set; }
        public double TPrice { get; set; }
    }
}
