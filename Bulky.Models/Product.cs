using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bulky.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(2, ErrorMessage = "2 or more letters")]
        [DisplayName("Product Name")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [DisplayName("List Price")]
        [Range(1,1000)]
        public double ListPrice { get; set; }


        [Required]
        [DisplayName("Price 1-49")]
        [Range(1, 1000)]
        public double Price { get; set; }
        [Required]
        [DisplayName("Price 50-99")]
        [Range(1, 1000)]
        public double Price50 { get; set; }
        [Required]
        [DisplayName("Price 100+")]
        [Range(1,1000)]
        public double Price100 { get; set; }

        public int CategoryId {  get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageURL { get; set; }
    }
}
