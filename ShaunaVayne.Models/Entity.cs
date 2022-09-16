using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ShaunaVayne.Models
{
    public class Entity
    {
        [Key]
        [BindNever]
        public Guid Id { get; set; } = Guid.NewGuid();


        [BindNever]
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    }
}