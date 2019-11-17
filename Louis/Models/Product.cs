using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Louis.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
        public string Photo { get; set; }

        public bool IsPriceConfirmed { get; set; }

        [Required]
        [Range(1, 10000000, ErrorMessage = "Value for price must be between {1} and {2}"), DataType(DataType.Currency)]
        [PriceValidation("IsPriceConfirmed", ErrorMessage ="price over 999 need to be confirmed.") ]
        public decimal Price { get; set; }

        

        [DataType(DataType.Date)]
        public DateTime LastUpdated { get; set; }


    }
}
