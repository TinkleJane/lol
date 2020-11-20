using System;
using System.ComponentModel.DataAnnotations;

namespace ShaunaVayne.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}