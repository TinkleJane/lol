using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShaunaVayne.Models
{
    [Table("Haha")]
    public class Test
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }


    public class Book:Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
    }
}