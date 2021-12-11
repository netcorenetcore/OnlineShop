using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Online.Shop.Core.Data.Abstarctions
{
    public abstract class EntityBase
    {
        [Key]
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
